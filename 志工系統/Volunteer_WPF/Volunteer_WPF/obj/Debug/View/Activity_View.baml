   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   &     Volunteer_WPF.View.Activity_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	     � Title$  Activity_View��5   	   $	��615��6   $
��1020��6,   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��51* �6   �� 5      $
��287* �6   .+�5   
   ]� -   5         ]�Name$ 
Dock_Frame��5      ��. $��1,1,0.6,0.6q�6=   $��2��6R   .+�5      �� $
��Left=�6   $	��150��61   $��1q�6>   $
��Left=�6I   .+�5      �� -   5         ��Name$ btn_addactivity��5      $��新增活動��62   $��35��6A   $��5q�6M   $��14(�6X   ���	#FFA7E6DA
�������   ButtonTemplate#���   6�   ��6}   Z� 6�   �� 5      $��活動日期��6   $��14(�6(   (  "System.Windows.Controls.DatePicker  5      -   5        $��dp_startdate��5      �� 5      $��~��6   $��16(�6%     5      -   5        $��
dp_enddate��5      �� 5      $��活動類別��6   $��14(�6(   �� 5      -   5        $��cbb_activitytype��5      $��3��63   $��28��6@   $��3q�6L   �� 5      $��活動組別��6   $��14(�6(   �� 5      -   5        $��cbb_activitygroup��5      $��3��64   $��28��6A   $��3q�6M   �� 5      -   5        $ btn_selectactivity��5      $��搜尋��65   $��35��6B   $��5q�6N   $��14(�6Y   ���	#FFA7E6DA
�������#���   6�   ��6~   Z� 6�   5      � 5      $	��584��6   $	��Top=�6    $	��858��68    � RowDefinitions 5      �� 5      $��* �6    �� 5      $��2* �6    .+�5       �� $��3��6   $��0��6*   $��Bottom=�67   $��Stretch=�6R   
������ 6p   &   System.Windows.Controls.DataGrid  5!      -   5          Name$ dg_activity��5!      $��1q�61      
IsReadOnly . $��14(�6N      Columns 5"      4  .System.Windows.Controls.DataGridTemplateColumn  5#         CellTemplate 5$      �� 5%   "   .R�5&   &   �� -	   5        $��修改��5&   -   $��50��6:   ���	#FFA7E6DA
���������6q   Z� 6�   5'   #   5)      5+      �� 5,      $
��Auto��6   $��2��6-   .��5-      �� -
   5        	 ��Name$	 wondow_show��5-   !   $
��Left=�66   $��10q�6Q   $��1��6]   $	��Top=�6j   5.      5/      51      52      53      