   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   -clr-namespace:Volunteer_WPFVolunteer_WPF  5   	   G(clr-namespace:Volunteer_WPF.User_ControlVolunteer_WPF.User_Control  5   	   %    Volunteer_WPF.View.Sign_up_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	   5Volunteer_WPFclr-namespace:Volunteer_WPF     5   	   AUser_Control(clr-namespace:Volunteer_WPF.User_Control     5   	     � Title$  Sign_up_View��5   	   $	��615��6   $
��1020��6+   .��5      � .+�5   
   ]� -   5         ]�Name$ 
Dock_Frame��5      ��. $��1q�6=   .+�5      �� -   5         ��Name$ StackPanel_Left��5      $
��Left=�62   $��1q�6H   $	��150��6S   
�������.+�5      �� -   5         ��Name$ btn_firstcheck��5      $��初次申請審核��61   $��35��6B   $��5q�6k   ���	#FFA7E6DA
�������$��Microsoft Sans Serif)�6�   $��14(�6�      ButtonTemplate#���   6�   ��6�   Z� 6�   �� 5      -   5        $ btn_interview��5      $��面試階段審核��60   $��35��6A   $��5q�6j   ���	#FFA7E6DA
�������$��Microsoft Sans Serif)�6�   $��14(�6�   #���   6�   ��6�   Z� 6�   �� 5      -   5        $ btn_Allpass��5      $��查詢已通過審核��6.   $��35��6@   $��5q�6g   ���	#FFA7E6DA
�������$��Microsoft Sans Serif)�6�   $��14(�6�   #���   6�   ��6�   Z� 6�   �� 5      -   5        $ 
btn_reject��5      $��查詢已拒絕審核��6-   $��35��6?   $��5q�6e   ���	#FFA7E6DA
�������$��Microsoft Sans Serif)�6�   $��14(�6�   #���   6�   ��6�   Z� 6�   �� 5      $��依日期查詢:��6   $��Microsoft Sans Serif)�6)   $��16(�6K   $
��Bold"�6Y   �� 5      $��起始日期:��6   $��Microsoft Sans Serif)�6(   $��14(�6J   (  "System.Windows.Controls.DatePicker  5      -   5        $��D_Start_date��5      �� 5      $��結束日期:��6   $��Microsoft Sans Serif)�6(   $��14(�6J     5      -   5        $��
D_End_date��5      �� 5      -	   5        $ btn_selectDate��5      $��查詢��61   $��35��6>   $��5q�6K   ���	#FFA7E6DA
�������$��Microsoft Sans Serif)�6�   $��14(�6�   #���   6�   ��6�   Z� 6�   �� 5      -
   5        $ btn_getExcel��5      $��匯出Excel��6/   $��35��6A   $��5q�6N   ���	#FFA7E6DA
�������$��Microsoft Sans Serif)�6�   $��14(�6�   #���   6�   ��6�   Z� 6�   �� 5      $��身分類別選擇:��6   $��Microsoft Sans Serif)�6*   $��16(�6L   $
��Bold"�6Z   	� 5      -   5        $��rbtn_society��5      $��社會志工��62   $��Microsoft Sans Serif)�6A   $��14(�6c    d� 	IsChecked$
 TrueY�6q   6�   	� 5      -   5        $��rbtn_student��5      $��學生志工��62   $��Microsoft Sans Serif)�6A   $��14(�6c   6q   5      � 5      $
��Auto��6    � ShowGridLines .  � RowDefinitions 5      �� 5       $	��39* �6$   �� 5!      $
��155* �6$   �� 5"      $
��388* �6$   .+�5$      �� $��1��6   $��Bottom=�6,   $��Stretch=�6H   $��2q�6f   
������ $��4��6�   6�   &   System.Windows.Controls.DataGrid  5(      -   5          Name$ 
DataGrid_1��5(      
�������   AutoGenerateColumns . $��Microsoft Sans Serif)�6�   $��14(�6�   	   CanUserAddRows	 .  $��1��6�   ��.  $��0,0,0,2q�6
  
   Columns
 5)      4  .System.Windows.Controls.DataGridTemplateColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 61      HeaderTemplate 5+      �� 5,   "   .R�5-   &   �� $�� ��60   
������ 68   5.   #      CellTemplate 50      �� 51   "   .R�52   &   �� -   5        $��刪除��52   -   $��50��6:   ���	#FFA7E6DA
���������6t   Z� 6�   53   #   55        56      $	 100 61    57      �� 58   "   .R�59   &   �� $��審核設定��60   
������ 6<   5:   #    5<      �� 5=   "   .R�5>   &   .   (Volunteer_WPF.User_Control.MyUserControl    Name$ Check_interview��6X   .   (Volunteer_WPF.User_Control.MyUserControl   MU_Click_OK Check_interview_Click6q      MU_Click_Delete# Check_interview_MU_Click_Delete6�   $��60��6A   $��35��6L   6�   5?   #   5A      5D      �� 5E      $
��Auto��6   $��2��6-   .��5F      �� -   5        $ wondow_show��5F   !   $
��Left=�66   $��10q�6Q   $��1��6]   $	��Top=�6j   5G      �� 5H      $��1q�6    �� Orientation$ 
Horizontal=�6(   .+�5L      �� -   5        $��確認��5L      $	��106��6B   $��Bottom=�6N   $
��Left=�6i   ���	#FFA7E6DA
�������$��Microsoft Sans Serif)�6�   $��14(�6�   $��35��6�   #���   6   ��6�   Z� 6   �� 5M      $��已勾選數:��6   $��Microsoft Sans Serif)�6,   $��16(�6N   $��10,5,5,0q�6\   �� 5N      -   5        $��	lab_check��5N      $��0��6-   $��Microsoft Sans Serif)�69   $��16(�6[   $��0,5,5,5q�6i   
���  ��6z   �� 5O      $��欲駁回數:��6   $��Microsoft Sans Serif)�6,   $��16(�6N   $��10,5,5,0q�6\   �� 5P      -   5        $��
lab_Dcheck��5P      $��0��6.   $��Microsoft Sans Serif)�6:   $��16(�6\   $��0,5,5,5q�6j   
���  ��6{   5T      5U      5W      5Y      5Z      