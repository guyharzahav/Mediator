using System;
using System.Collections.Generic;
using Mediator.KnowledgeService;
using Mediator.DataService;
using System.Globalization;
using System.Linq;

delegate double Func(double n1, double n2);
namespace Mediator.communication
{
    class Services
    {
        private  KnowledgeLibraryWSSoapClient knowledgeClient;
        private aKontrollerSoapClient dataClient;
        private const int KBID = 90;
        private const int PROJECT_ID = 2;
        

        public Services()
        {
            knowledgeClient = new KnowledgeLibraryWSSoapClient("KnowledgeLibraryWSSoap");
            dataClient = new aKontrollerSoapClient("aKontrollerSoap");
        }


        // get the Knowledge items from knowledgeBase with contype and outtype (default - Primitive and Numeric)
        public LinkedList<KnowledgeService.KnowledgeItem> getKnowledgeItemsByConceptAndOutput(KnowledgeService.ConceptTypes conType = KnowledgeService.ConceptTypes.Primitive
            , KnowledgeService.OutputTypes outType = KnowledgeService.OutputTypes.Numeric) 
        {
            LinkedList<KnowledgeService.KnowledgeItem> resItems = new LinkedList<KnowledgeService.KnowledgeItem>();
            KnowledgeBase knoBase = knowledgeClient.GetKnowledgeBase(KBID);
            foreach (KnowledgeService.KnowledgeItem item in knoBase.knowledgeItems) 
            {
                if (item.ConceptType == conType && item.OutputType == outType)
                    resItems.AddFirst(item);
            }
            return resItems;
        }

        //get all the data points according to all the knowledge items
        public LinkedList<DataPoint> getDataPoints() 
        {
            LinkedList<DataPoint> dataPoints = new LinkedList<DataPoint>();
            foreach (KnowledgeService.KnowledgeItem item in getKnowledgeItemsByConceptAndOutput())
            {
                foreach (DataPoint data in dataClient.GetDataByConcept(PROJECT_ID, item.Title)) 
                {
                    dataPoints.AddFirst(data);
                }
            }
            return dataPoints;
        }




        // ---------------------------------------------------------------Task 2 - Naive algorithm---------------------------------------------------------------
        public List<IntersectionValue> calcState(KnowledgeService.KnowledgeItem KB1, KnowledgeService.KnowledgeItem KB2, Func func)
        {
            List<IntersectionValue> calcs = new List<IntersectionValue>();
            DataPoint[] DB1 = dataClient.GetDataByConcept(PROJECT_ID, KB1.Title);
            DataPoint[] DB2 = dataClient.GetDataByConcept(PROJECT_ID, KB2.Title);
            setArraysNewTimes(DB1, KB1);
            setArraysNewTimes(DB2, KB2);
            foreach (DataPoint data1 in DB1) 
            {
                foreach (DataPoint data2 in DB2) 
                {
                    if (dataPointsMeet(data1, data2))
                        calcs.Add(GenerateInterValue(data1, data2, func));
                }
            }
            return calcs;
        }

        // generate new interValue obkect according to the intersection of d1 and d2.
        private IntersectionValue GenerateInterValue(DataPoint d1, DataPoint d2, Func func)
        {
            DateTime start = d2.StartTime;
            DateTime end = d2.EndTime;
            double value = func(double.Parse(d1.Value), double.Parse(d2.Value));
            if (d1.StartTime > d2.StartTime)
                start = d1.StartTime;
            if (d1.EndTime < d2.EndTime)
                end = d1.EndTime;
            return new IntersectionValue(value, start, end);
        }

        // set the new time for dataPoint data according to the KB features
        private void setNewTimes(DataPoint data, KnowledgeService.KnowledgeItem KB) 
        {
            string timeUnit = KB.LocalPersistencyTimeUnit;
            data.StartTime = getNewDateTime(timeUnit, data.StartTime, KB.GoodBefore, true);
            data.EndTime = getNewDateTime(timeUnit, data.EndTime, KB.GoodAfter, false);
        }

        // check if d1 and d2 meets for efficient method
        private bool dataPointsMeet(DataPoint d1, DataPoint d2)
        {
            return d1.StartTime <= d2.EndTime && d1.EndTime >= d2.StartTime;
        }

        //get the new time added based on oldDate and the addedTime
        private DateTime getNewDateTime(string timeunit, DateTime oldDate, string addedTime, bool isBefore) 
        {
            DateTime retTime;
            int toAdd;
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            if (isBefore)
            {
                toAdd = (-1) * int.Parse(addedTime);
            }
            else 
            { 
                toAdd = int.Parse(addedTime);
            }
            if (checkDateLimits(oldDate, toAdd,  timeunit))
                return oldDate;

            switch (timeunit.ToLower()) 
            {
                case "years":
                    retTime = oldDate.AddYears(toAdd);
                    break;
                case "months":
                    retTime = oldDate.AddMonths(toAdd);
                    break;
                case "weeks":
                    retTime = myCal.AddWeeks(oldDate, toAdd);
                    break;
                case "days":
                    retTime = oldDate.AddDays(toAdd);
                    break;
                case "hours":
                    retTime = oldDate.AddHours(toAdd);
                    break;
                case "minutes":
                    retTime = oldDate.AddMinutes(toAdd);
                    break;
                case "seconds":
                    retTime = oldDate.AddSeconds(toAdd);
                    break;
                default:
                    retTime = oldDate;
                    break;
            }
            return retTime;
        }

        public static double Mult(double n1, double n2) { return n1 * n2; }
        public static double Div(double n1, double n2) { return n1 / n2; }
        public static double Plus(double n1, double n2) { return n1 + n2; }
        public static double Minus(double n1, double n2) { return n1 - n2; }





        // ---------------------------------------------------------------Task 2 - Simple less efficient algorithm---------------------------------------------------------------
        public List<IntersectionValue> calcStateEff(KnowledgeService.KnowledgeItem KB1, KnowledgeService.KnowledgeItem KB2, Func func)
        {
            List<IntersectionValue> calcs = new List<IntersectionValue>();
            DataPoint[] DB1 = dataClient.GetDataByConcept(PROJECT_ID, KB1.Title);
            DataPoint[] DB2 = dataClient.GetDataByConcept(PROJECT_ID, KB2.Title);
            setArraysNewTimes(DB1, KB1);
            setArraysNewTimes(DB2, KB2);
            DataPointIndex[] dataIndexArr1 = ToDataIndexArr(DB1, 1);
            DataPointIndex[] dataIndexArr2 = ToDataIndexArr(DB2, 2);
            DataPointIndex[] conDataIndexPoints = new DataPointIndex[dataIndexArr1.Length + dataIndexArr2.Length];
            dataIndexArr1.CopyTo(conDataIndexPoints, 0);
            dataIndexArr2.CopyTo(conDataIndexPoints, DB1.Length);
            Array.Sort(conDataIndexPoints, (x, y) => x.StartTime.CompareTo(y.StartTime));

            for (int i = 0; i < conDataIndexPoints.Length; i++)
            {
                for (int j = i+1; j < conDataIndexPoints.Length; j++)
                {
                    if (conDataIndexPoints[i].From != conDataIndexPoints[j].From && dataIndexPointsMeet(conDataIndexPoints[i], conDataIndexPoints[j]))
                        calcs.Add(GenerateInterValueFromIndex(conDataIndexPoints[i], conDataIndexPoints[j], func));
                    else if (conDataIndexPoints[j].StartTime > conDataIndexPoints[i].EndTime)
                        break;
                }
            }
            return calcs;
        }

        private IntersectionValue GenerateInterValueFromIndex(DataPointIndex dataPointIndex1, DataPointIndex dataPointIndex2, Func func)
        {
            DateTime start = dataPointIndex2.StartTime;
            DateTime end = dataPointIndex2.EndTime;
            double value = func(dataPointIndex1.Value, dataPointIndex2.Value);
            if (dataPointIndex1.StartTime > dataPointIndex2.StartTime)
                start = dataPointIndex1.StartTime;
            if (dataPointIndex1.EndTime < dataPointIndex2.EndTime)
                end = dataPointIndex1.EndTime;
            return new IntersectionValue(value, start, end);
        }

        private bool dataIndexPointsMeet(DataPointIndex dataPointIndex1, DataPointIndex dataPointIndex2)
        {
            return dataPointIndex1.StartTime <= dataPointIndex2.EndTime && dataPointIndex1.EndTime >= dataPointIndex2.StartTime;
        }

        //gets array of data points and set to each one the new time interval
        private void setArraysNewTimes(DataPoint[] DB, KnowledgeService.KnowledgeItem KB)
        {
            foreach (DataPoint data in DB)
            {
                setNewTimes(data, KB);
            }
        }

        public bool checkDateLimits(DateTime oldDate, int toAdd, string timeunit) 
        {
            return (oldDate.Year == 1 && toAdd == -1 && timeunit.ToLower() == "years") || (oldDate.Year == 9999 && toAdd == 1 && timeunit.ToLower() == "years");
        }

        public DataPointIndex ToDataIndex(DataPoint data, int from) 
        {
            return new DataPointIndex(data.StartTime, data.EndTime, double.Parse(data.Value), from);
        }

        public DataPointIndex[] ToDataIndexArr(DataPoint[] DB, int from) 
        {
            DataPointIndex[] retArr = new DataPointIndex[DB.Length];
            int i = 0;
            foreach (DataPoint data in DB) 
            {
                retArr[i] = ToDataIndex(data, from);
                i++;
            }
            return retArr;
        }





        // ---------------------------------------------------------------Task 2 - Complex Efficient algorithm---------------------------------------------------------------
        public LinkedList<IntersectionValue> calcStateEffComplex(KnowledgeService.KnowledgeItem KB1, KnowledgeService.KnowledgeItem KB2, Func func)
        {
            Dictionary<long, DataLink> openLinksMap1 = new Dictionary<long, DataLink>();
            Dictionary<long, DataLink> openLinksMap2 = new Dictionary<long, DataLink>();
            LinkedList<IntersectionValue> calcs = new LinkedList<IntersectionValue>();
            DataPoint[] DB1 = dataClient.GetDataByConcept(PROJECT_ID, KB1.Title);
            DataPoint[] DB2 = dataClient.GetDataByConcept(PROJECT_ID, KB2.Title);
            setArraysNewTimes(DB1, KB1);
            setArraysNewTimes(DB2, KB2);
            DataLink[] dataLinkArr1 = toDataLinkArray(DB1, 1);
            DataLink[] dataLinkArr2 = toDataLinkArray(DB2, 2);
            DataLink[] conDataLink = new DataLink[dataLinkArr1.Length + dataLinkArr2.Length];
            dataLinkArr1.CopyTo(conDataLink, 0);
            dataLinkArr2.CopyTo(conDataLink, dataLinkArr1.Length);

            Array.Sort(conDataLink, (x, y) => x.RealTime.CompareTo(y.RealTime));

            foreach (DataLink data in conDataLink)
            {
                if (data.IsOpen && data.From == 1)
                    openLinksMap1.Add(data.Id, data);
                else if (data.IsOpen && data.From == 2)
                    openLinksMap2.Add(data.Id, data);
                else if (data.IsOpen == false && data.From == 1)
                {
                    concatLists(calcs, genInterVal(openLinksMap2, data, func));
                    openLinksMap1.Remove(data.Id);
                }
                else if (data.IsOpen == false && data.From == 2)
                {
                    concatLists(calcs, genInterVal(openLinksMap1, data, func));
                    openLinksMap2.Remove(data.Id);
                }
            }
            return calcs;
        }

        public LinkedList<IntersectionValue> genInterVal(Dictionary<long, DataLink> openLinksMap, DataLink closingData, Func func) 
        {
            LinkedList<IntersectionValue> retList = new LinkedList<IntersectionValue>();
            DateTime EndTime = closingData.RealTime;
            DateTime StartTime = closingData.OtherTime;
            foreach (KeyValuePair<long, DataLink> entry in openLinksMap)
            {
                if (closingData.OtherTime < entry.Value.RealTime)
                    StartTime = entry.Value.RealTime;
                retList.AddFirst(new IntersectionValue(func(entry.Value.Value, closingData.Value), StartTime, EndTime));
            }
            return retList;
        }

        public void concatLists(LinkedList<IntersectionValue> list1, LinkedList<IntersectionValue> list2) 
        {
            foreach (IntersectionValue interval in list2) 
            {
                list1.AddFirst(interval);
            }
        }


        public DataLink toOpenDataLink(DataPoint data, long id, int from)
        {
            return new DataLink(data.StartTime, data.EndTime, true, double.Parse(data.Value), id, from);
        }

        public DataLink toCloseDataLink(DataPoint data, long id, int from)
        {
            return new DataLink(data.EndTime, data.StartTime, false, double.Parse(data.Value), id, from);
        }

        public DataLink[] toDataLinkArray(DataPoint[] DB, int from)
        {
            long id = 0;
            int i = 0;
            DataLink[] retArr = new DataLink[2 * DB.Length];
            foreach (DataPoint data in DB)
            {
                DataLink openDL = toOpenDataLink(data, id, from);
                DataLink closeDL = toCloseDataLink(data, id, from);
                retArr[i] = openDL;
                retArr[i + 1] = closeDL;
                i += 2;
                id++;
            }
            return retArr;
        }
    }
}
