   M S B A M L   `   `   `  ����C  ?Mediator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MediatorMediator  5   	   ?$clr-namespace:Mediator.communicationMediator.communication  5   	   E'clr-namespace:Mediator.KnowledgeServiceMediator.KnowledgeService  5   	   ;"clr-namespace:Mediator.DataServiceMediator.DataService  6J   7 clr-namespace:Mediator.ViewModelMediator.ViewModel  6�       Mediator.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Mediator     5   	   =InterService$clr-namespace:Mediator.communication     5   	   DKnowledgeService'clr-namespace:Mediator.KnowledgeService     5   	   :DataService"clr-namespace:Mediator.DataService     6J   3local1 clr-namespace:Mediator.ViewModel     6�   -   5          � Title$  
MainWindow��5
   	   $��584.022��6   $��1352.94��6-    � 	Resources 5         knowledgeItemViewSource  dataPointViewSource  interValueViewSource��%W   &	        &	      &	 &     �� 5   
   6   �� 5   
   6   �� 5   
   6   5      .��5      �� $��10,0,0,0q�6   .@�5   
   �� $��Knowledge Items��6   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ knowledgeItemDataGrid��5         AutoGenerateColumns .     EnableRowVirtualization . $��0,10,2,3q�6q      RowDetailsVisibilityMode$ VisibleWhenSelected=�6�   ��6�   ��  �� Source# [     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        conceptTypeColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Concept Type��5   d   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 SizeToHeader 6z   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6D   �� *ConceptType+6z   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      -   5        existsInEMRColumn��$ Exists In EMR��5   h   $	 SizeToHeader 6   
 6H   �� *ExistsInEMR+6     5      -   5        goodAfterColumn��$ 
Good After��5   `   $	 SizeToHeader 6t   
 6B   �� *	GoodAfter+6t     5      -   5        goodBeforeColumn��$ Good Before��5   b   $	 SizeToHeader 6w   
 6C   �� *
GoodBefore+6w     5      -   5        iDColumn��$ ID��5   R   $	 SizeToHeader 6^   
 6;   �� *ID+6^     5      -   5        isLogicOnlyColumn��$ Is Logic Only��5   h   $	 SizeToHeader 6   
 6H   �� *IsLogicOnly+6     5      -	   5        isOrdinalColumn��$ 
Is Ordinal��5   d   $	 SizeToHeader 6x   
 6F   �� *	IsOrdinal+6x     5      -
   5        isTemporalColumn��$ Is Temporal��5   f   $	 SizeToHeader 6{   
 6G   �� *
IsTemporal+6{     5      -   5        
kBIDColumn��$
 KBID��5   V   $	 SizeToHeader 6d   
 6=   �� *KBID+6d     5      -   5        	kFNColumn��$	 KFN��5   T   $	 SizeToHeader 6a   
 6<   �� *KFN+6a     5      -   5        "localPersistencyTimeUnitColumn��$! Local Persistency Time Unit��5   ~   $	 SizeToHeader 6�   
 6Q   �� *LocalPersistencyTimeUnit+6�     5      -   5        logicTypeColumn��$ 
Logic Type��5   `   $	 SizeToHeader 6t   
 6B   �� *	LogicType+6t     5       -   5        mapLocationXColumn��$ Map Location X��5    f   $	 SizeToHeader 6~   
 6E   �� *MapLocationX+6~     5!      -   5        mapLocationYColumn��$ Map Location Y��5!   f   $	 SizeToHeader 6~   
 6E   �� *MapLocationY+6~     5"      -   5        outputTypeColumn��$ Output Type��5"   b   $	 SizeToHeader 6w   
 6C   �� *
OutputType+6w     5#      -   5        outputUnitTypeColumn��$ Output Unit Type��5#   j   $	 SizeToHeader 6�   
 6G   �� *OutputUnitType+6�     5$      -   5        #persistancetTableTimeUnitColumn��$" Persistancet Table Time Unit��5$   �   $	 SizeToHeader 6�   
 6R   �� *PersistancetTableTimeUnit+6�     5%      -   5        persistancetTableTypeColumn��$ Persistancet Table Type��5%   x   $	 SizeToHeader 6�   
 6N   �� *PersistancetTableType+6�     5&      -   5        scaleMaxValueColumn��$ Scale Max Value��5&   h   $	 SizeToHeader 6�   
 6F   �� *ScaleMaxValue+6�     5'      -   5        scaleMinValueColumn��$ Scale Min Value��5'   h   $	 SizeToHeader 6�   
 6F   �� *ScaleMinValue+6�     5(      -   5        scaleTitleColumn��$ Scale Title��5(   b   $	 SizeToHeader 6w   
 6C   �� *
ScaleTitle+6w     5)      -   5        semiFormalColumn��$ Semi Formal��5)   b   $	 SizeToHeader 6w   
 6C   �� *
SemiFormal+6w     5*      -   5        serverIDColumn��$ 	Server ID��5*   ^   $	 SizeToHeader 6q   
 6A   �� *
ServerID+6q     5+      -   5        sturcturedTextColumn��$ Sturctured Text��5+   j   $	 SizeToHeader 6�   
 6G   �� *SturcturedText+6�     5,      -   5        titleColumn��$ Title��5,   X   $	 SizeToHeader 6g   
 6>   �� *Title+6g     5-      -   5        viewOnlyColumn��$ 	View Only��5-   b   $	 SizeToHeader 6u   
 6E   �� *
ViewOnly+6u   5/      50      �� 51   
   $��Data Points��6   .��52        -   5        $ dataPointDataGrid��52       .   . $ VisibleWhenSelected=�6�   $��
98,0,951,0q�6�   ��6m   �� # [  53      4  .System.Windows.Controls.DataGridTemplateColumn  54      -   5        endTimeColumn��$ End Time��54   D   $	 SizeToHeader 6V      CellTemplate 55      �� 56      .R�57   "   (  "System.Windows.Controls.DatePicker     SelectedDate 6-   �� *	EndTime+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 58      5:        5;      -   5        patientIDColumn��$ 
Patient ID��5;   `   $	 SizeToHeader 6t   
 6B   �� *	PatientID+6t     5<      -    5        startTimeColumn��$ 
Start Time��5<   F   $	 SizeToHeader 6Z    5=      �� 5>      .R�5?   "      6-   �� *	StartTime+$ TwoWay=� .  . 5@      5B        5C      -!   5        valueColumn��$ Value��5C   X   $	 SizeToHeader 6g   
 6>   �� *Value+6g   5E      5F      �� 5G   
   $��Intersection Values��6   .��5H        -"   5        $ intersectionValueDataGrid��5H       .   . $ VisibleWhenSelected=�6�   $��
98,0,951,0q�6�   ��6u   �� # [  5I        5J      -#   5        interEndTimeColumn��$ End Time��5J   I   $	 SizeToHeader 6[    5K      �� 5L      .R�5M   "      6-   �� *	EndTime+$ TwoWay=� .  . 5N      5P        5Q      -$   5        interStartTimeColumn��$ 
Start Time��5Q   K   $	 SizeToHeader 6_    5R      �� 5S      .R�5T   "      6-   �� *	StartTime+$ TwoWay=� .  . 5U      5W        5X      -%   5        interValueColumn��$ Value��5X   ]   $	 SizeToHeader 6l   
 6C   �� *Value+6l   5Z      5[      5\      5]      