   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   C&clr-namespace:Volunteer_WPF.View_ModelVolunteer_WPF.View_Model  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
   	   G(clr-namespace:Volunteer_WPF.User_ControlVolunteer_WPF.User_Control  5   	   6    0Volunteer_WPF.View.Activity_AddActionDetail_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	   4t&clr-namespace:Volunteer_WPF.View_Model     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bic9http://schemas.microsoft.com/expression/2010/interactions  5	   	   0sys&clr-namespace:System;assembly=mscorlib  5
   	   ;User_Control(clr-namespace:Volunteer_WPF.User_Control  5   	     � Title$#  Activity_AddActionDetail_View��5   	   $	��615��6/   $
��1020��6<    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $, &Dictionary_ValidationErrorTemlate.xaml?�6%   6W   %    5      .��5      (�  (� Stretch$
 Fill=�6   .��5   
   � ���	#FFA7E6DA$	��586��6&   $
��1014��63    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ lab_ActName��5      $��活動名稱��6)   $��Microsoft Sans Serif)�68   $��16(�6Z   $��10,10,416,0q�6h   $��34��6}   $	��Top=�6�   �� 5      -   5         ��Name$ tbx_ActName��5      $
��Left=�6+   $��25��6F   $��
125,10,0,0q�6R    �� TextWrapping$
 Wrap=�6f      validationErrorTemplate1	 =� ErrorTemplate#	 [  6z   $	��Top=�6h  $	��300��6�  $��Microsoft Sans Serif)�6�  $��16(�6�  ��6�   �� *VM_Activity_name+
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors .  �� ValidatesOnExceptions .  �� NotifyOnValidationError . 6�  �� 5      -   5        $ lab_ActType��5      $��活動類別��6)   $��Microsoft Sans Serif)�68   $��16(�6Z   $��10,44,416,0q�6h   $��34��6}   $	��Top=�6�   �� 5       -   5         ��Name$ cmb_ActType��5       $
��Left=�6,   $��
125,40,0,0q�6G   $	��Top=�6[   $	��100��6s   $��34��6   $��Microsoft Sans Serif)�6�   $��16(�6�    �� DisplayMemberPath$ Activity_type��6�   ��6�   �� *TM+ �� Text 6�   �� *VM_Activity_type+.  (System.Windows.Interactivity.Interaction   Triggers 5"      /  )System.Windows.Interactivity.EventTrigger  5#         	EventName$ SelectionChanged��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 5$      >  8Microsoft.Expression.Interactivity.Core.CallMethodAction     
MethodName$" cmb_ActType_SelectionChanged��6G      TargetObject 6.   �� 6G   5%      5'      �� 5(      -   5        $ lab_ActGroupID��5(      $��活動組別��6,   $��Microsoft Sans Serif)�6;   $��16(�6]   $��10,83,416,0q�6k   $��34��6�   $	��Top=�6�   �� 5)      -   5        $ cmb_ActGroupID��5)      $
��Left=�6/   $��
125,79,0,0q�6J   $	��Top=�6^   $	��100��6v   $��34��6�   $��Microsoft Sans Serif)�6�   $��16(�6�   $ 
Group_name��6�   ��6�   �� *GM+ 6�   �� *VM_Group_name+ 5*        5+      $ SelectionChanged��6%   . 5,        $% cmb_ActGroupID_SelectionChanged��6G    6.   �� 6G   5-      5/      �� 50      -   5        $ lab_ActAddresser��50      $��講師��6.   $��Microsoft Sans Serif)�6;   $��16(�6]   $��41,224,424.2,0q�6k   $��34��6�   $	��Top=�6�    J� RenderTransformOrigin$ -0.131,-1.5&�6�   �� 51      -   5        $ tbx_ActOrganization��51      $
��Left=�63   $��25��6N   $��125,224,0,0q�6Z   $
 Wrap=�6o   $	��Top=�6�   $	��300��6�   $��Microsoft Sans Serif)�6�   $��16(�6�   ��6�   �� *VM_lecturer+6�   �� 52      -	   5        $ lab_ActTaker��52      $��	承辦人��6*   $��Microsoft Sans Serif)�68   $��16(�6Z   $��29,251,416,0q�6h   $��34��6~   $	��Top=�6�   �� 53      -
   5        $ tbx_ActTaker��53      $
��Left=�6,   $��25��6G   $��125,254,0,0q�6S   $
 Wrap=�6h   $	��Top=�6�   $	��300��6�   $��Microsoft Sans Serif)�6�   $��16(�6�    �� 
IsReadOnly . ��6|   �� *VM_supervision_Name+6�   �� 54      -   5        $ lab_ActOrganizationPhone��54      $��承辦單位電話��66   $��Microsoft Sans Serif)�6G   $��16(�6i   $��0,290,396,0q�6w   $��34��6�   $	��Top=�6�   �� 55      -   5        $ tbx_ActOrganizationPhone��55      $
��Left=�68   $��25��6S   $��125,290,0,0q�6_   $
 Wrap=�6t   $	��Top=�6�   $	��300��6�   $��Microsoft Sans Serif)�6�   $��16(�6�    . ��6�   �� *VM_supervision_phone+6  �� 56      -   5        $ lab_ActOrganizationEmail��56      $��承辦單位E-Mail��66   $��Microsoft Sans Serif)�6K   $��16(�6m   $��0,324,386,0q�6{   $��34��6�   $	��Top=�6�   �� 57      -   5        $ tbx_ActOrganizationEmail��57      $
��Left=�68   $��25��6S   $��125,324,0,0q�6_   $
 Wrap=�6t   $	��Top=�6�   $	��300��6�   $��Microsoft Sans Serif)�6�   $��16(�6�    . ��6�   �� *VM_supervision_email+6  �� 58      -   5        $ lab_ActCourseMemberMax��58      $��課程人數��64   $��Microsoft Sans Serif)�6C   $��16(�6e   $��12,363,416,0q�6s   $��34��6�   $	��Top=�6�   �� 59      -   5        $ tbx_ActCourseMemberMax��59      $
��Left=�66   $��25��6Q   $��125,363,0,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $	��300��6�   $��Microsoft Sans Serif)�6�   $��16(�6�   ��6�   �� *	VM_Member+6�   �� 5:      -   5        $ lab_ActCourseStandbyMax��5:      $��備取人數��65   $��Microsoft Sans Serif)�6D   $��16(�6f   $��9,402,416,0q�6t   $��34��6�   $	��Top=�6�   �� 5;      -   5        $ tbx_ActCourseStandbyMax��5;      $
��Left=�67   $��25��6R   $��125,402,0,0q�6^   $
 Wrap=�6s   $	��Top=�6�   $	��300��6�   $��Microsoft Sans Serif)�6�   $��16(�6�   ��6�   �� *
VM_Spare+6�   �� 5<      -   5        $ lab_ActPlace��5<      $��活動地點��6*   $��Microsoft Sans Serif)�69   $��16(�6[   $��9,117,416,0q�6i   $��34��6~   $	��Top=�6�   �� 5=      -   5        $ tbx_ActPlace��5=      $
��Left=�6,   $��25��6G   $��125,118,0,0q�6S   $
 Wrap=�6h   $	��Top=�6�   $	��300��6�   $��Microsoft Sans Serif)�6�   $��16(�6�   $ 	0.48,0.52&�6�   ��6|   �� *
VM_Place+6�   �� 5>      -   5        $ lab_ActStartTime��5>      $��開始日期��6.   $��Microsoft Sans Serif)�6=   $��16(�6_   $��10,151,416,0q�6m   $��34��6�   $	��Top=�6�   (  "System.Windows.Controls.DatePicker  5?      -   5          Name$ datepicker_start��5?      $
��Left=�63   $��125,148,0,0q�6N   $	��Top=�6c   $	��138��6{   $��34��6�   
 �� Now   DisplayDateStart# Z 6�   #	 [  6�      SelectedDateFormat$ Short=�6�     SelectedDate 6  �� *VM_Activity_startdate+$
 TwoWay=� .  .  .  5@        5A      $ SelectedDateChanged��6%   . 5B        $* $datepicker_start_SelectedDateChanged��6G    6.   �� 6G   5C      5E      �� 5F      -   5        $ lab_ActEndTime��5F      $��結束日期��6,   $��Microsoft Sans Serif)�6;   $��16(�6]   $��12,185,416,0q�6k   $��34��6�   $	��Top=�6�     5G      -   5        $ datepicker_end��5G      $
��Left=�61   $��125,187,0,0q�6L   $	��Top=�6a   $	��138��6y   $��34��6�   # Z 6�   #	 [  6�    6  �� *VM_Activity_enddate+$
 TwoWay=� .  .  .  5H        5I      $ SelectedDateChanged ��6%   . 5J        $) #datepicker_end_SelectedDateChanged ��6H    6.   �� 6H   5K      5M      � 5N      $��1��6    � RowDefinitions 5O      �� 5P      $��* �6$   �� 5Q      $��* �6$   .+�5S      �� -   5        $ lab_ActSummary��5S      $��活動簡介��60   $��Microsoft Sans Serif)�6@   $��16(�6b   $��10,10,415,0q�6p   $��29��6�   $	��Top=�6�   �� 5T      -   5         ��Name$ btn_openFile��5T      $��開啟檔案��6/   $��18(�6U   $
��Bold"�6c   
�����@�
�������$
��Left=�6�   $��
165,17,0,0q�6�   $	��Top=�6�   $��78��6�   $��19��6�   $��Microsoft Sans Serif)�6�   ��6>   Z�  5U        5V      $ Click��6)   . 5W        $ btn_openFile_Click��6K    62   �� 6K   5X      5Z      �� 5[      -   5        $ lab_ActImage��5[      $��活動照片��6.   $��Microsoft Sans Serif)�6=   $��16(�6_   $��257,7,174,0q�6m   $��29��6�   $	��Top=�6�   �� 5\      -   5        $ btn_openImage��5\      $��開啟檔案��60   $��18(�6V   $
��Bold"�6d   
�����@�
�������$
��Left=�6�   $��
418,17,0,0q�6�   $	��Top=�6�   $��78��6�   $��19��6�   $��Microsoft Sans Serif)�6�   ��6?   Z�  5]        5^      $ Click��6)   . 5_        $ btn_openImage_Click��6K    62   �� 6K   5`      5b      �� 5c      -   5        $ tbx_ActSummary��5c      $
��Left=�62   $	��238��6M   $��	10,44,0,0q�6Z   $
 Wrap=�6m   $	��Top=�6�   $	��233��6�   $��Microsoft Sans Serif)�6�   $��20(�6�   ��6�   �� *
VM_Summary+6�   �� 5d      -   5          ��Name$  image��5d      $
��Left=�6'   $	��238��6B   $��
257,44,0,0q�6O   $	��Top=�6c   $	��239��6{   $
��Fill=�6�   ! J� 	AllowDrop! . ��6�   �� *VM_Activity_photos+ 5e        5f      $
 Drop��6)   . 5g        $ 
image_Drop��6K    62   �� 6K   5h      5j      �� 5k      $
��Left=�6   $	��215��68   $��10,10.2,0,0q�6E   $��1��6Z   $	��Top=�6g   $	��486��6   " �� Orientation$" 
Horizontal=�6�   .+�5l      �� $	��384��6#   $��5q�6/   $��Visible=�6:   $��Visible=�6a   .��5m      � -   5        # �Name$# WrapPanel_Viewphoto��5m   $   
�������$ � Orientation$$ 
Horizontal=�6T   ! .  5n        5o   "   $ 	DragEnter��61   . 5p   &     $# WrapPanel_Viewphoto_DragEnter��6S    6:   �� 6S   5q   #     5r   "   $
 Drop��61   . 5s   &     $ WrapPanel_Viewphoto_Drop��6S    6:   �� 6S   5t   #   5v      5w      �� 5x      $	��100��6!   ���	#FFA7E6DA$" Vertical=�6D   .+�5y      �� -    5        $ btn_amend_addimage��5y   !   $
��Bold"�6U   
�����@�
�������$��開啟檔案��6�   $��10q�6�   $��Microsoft Sans Serif)�6�   $��16(�6�   ��6=   Z�  5z        5{   "   $ Click��61   . 5|   &     $ btn_amend_addimage_Click��6S    6:   �� 6S   5}   #   5      �� 5�      -!   5        $ btn_amend_delete��5�   !   $
��Bold"�6R   
�����@�
�������$��刪除勾選��6�   $��10q�6�   $��Microsoft Sans Serif)�6�   $��16(�6�   ��6;   Z�  5�        5�   "   $ Click��61   . 5�   &     $ btn_amend_delete_Click��6S    6:   �� 6S   5�   #   5�      5�      5�      �� 5�      -"   5        $ btn_addActivity��5�      $��新增活動��62   $��18(�6X   $
��Bold"�6f   
�����@�
�������$
��Left=�6�   $��10,245.2,0,0q�6�   $	��Top=�6�   $	��106��6�   $��38��6�   $��Microsoft Sans Serif)�6  $��1��6%  ��6A   Z� {�62  �� *Addact+ 5�        5�      $ Click��6)   . 5�        $ btn_addActivity_Click��6K    62   �� 6K   5�      5�      �� 5�      -#   5        $ btn_amendact��5�      $��修改活動��6/   $��18(�6U   $
��Bold"�6c   
�����@�
�������$
��Left=�6�   $��137,245.2,0,0q�6�   $	��Top=�6�   $	��106��6�   $��38��6�   $��Microsoft Sans Serif)�6  $��1��6$  ��6>   Z� {�61  �� *
Amendact+ 5�        5�      $ Click��6)   . 5�        $ btn_amendact_Click��6K    62   �� 6K   5�      5�      �� 5�      -$   5        $
��DEMO��5�      $
��Left=�6(   $��18(�6Z   $
��Bold"�6h   
�����@�
�������$��269,245.2,0,0q�6�   $��1��6�   $	��Top=�6�   $	��106��6�   $��38��6�   ��6C   Z� 6�   5�      5�      5�      5�      