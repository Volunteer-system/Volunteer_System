   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   '    !Volunteer_WPF.View.Abnormal_event   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	     � Title$  Abnormal_event��5   	   $	��615��6    $
��1020��6-   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��51* �6   �� 5      $
��287* �6   .+�5   
   ]� -   5         ]�Name$ 
Dock_Frame��5      ��. $��1,1,0.6,0.6q�6=   $��2��6R   .+�5      �� $
��Left=�6   $	��150��61   $��1q�6>   $
��Left=�6I   .+�5      �� -   5         ��Name$ btn_newevent��5      $��	新事件��6/   $��35��6=   $��5q�6I   $��14(�6T   ���	#FFA7E6DA
�������   ButtonTemplate#���   6�   ��6y   Z� 6�   �� 5      -   5        $ 
btn_review��5      $��成案審核��6-   $��35��6<   $��5q�6H   $��14(�6S   ���	#FFA7E6DA
�������#���   6�   ��6x   Z� 6�   �� 5      -   5        $ btn_processing��5      $��	處理中��61   $��35��6?   $��5q�6K   $��14(�6V   ���	#FFA7E6DA
�������#���   6�   ��6{   Z� 6�   �� 5      -   5        $ btn_endprocessing��5      $��事件完成��64   $��35��6C   $��5q�6O   $��14(�6Z   ���	#FFA7E6DA
�������#���   6�   ��6   Z� 6�   �� 5      $��事件類別��6   $��14(�6(   �� 5      -   5        $��cbb_category��5      $��28��60   $��3q�6<   �� 5      $��運用單位��6   $��14(�6(   �� 5      -   5        $��cbb_application_unit��5      $��28��67   $��3q�6C   �� 5      $��事件日期��6   $��14(�6(   (  "System.Windows.Controls.DatePicker  5      -   5        $��dp_startdate��5      �� 5      $��~��6   $��16(�6%     5      -	   5        $��
dp_enddate��5      �� 5      -
   5        $ btn_selectabnormal_event��5      $��搜尋��6;   $��35��6H   $��5q�6T   $��14(�6_   ���	#FFA7E6DA
�������#���   6�   ��6�   Z� 6�   5      � 5      $	��584��6   $	��Top=�6    $	��858��68    � RowDefinitions 5      �� 5       $��* �6$   �� 5!      $��2* �6$   .+�5#      �� $��3��6   $��0��6*   $��Bottom=�67   $��Stretch=�6R   
������ 6p   &   System.Windows.Controls.DataGrid  5$      -   5          Name$ dg_abnormal_event��5$         
IsReadOnly . $��1q�6H      CanUserAddRows .  $��14(�6j      Columns 5%      4  .System.Windows.Controls.DataGridTemplateColumn  5&      	   CellTemplate	 5'      �� 5(   "   .R�5)   &   �� -   5        $��編輯��5)   -   $��50��6:   ���	#FFA7E6DA
���������6r   Z� 6�   5*   #   5,      5.      �� 5/      $
��Auto��6   $��1��6-   .��50      �� -   5        
 ��Name$
 wondow_show��50   !   $
��Left=�66   $��10q�6Q   $��1��6]   $	��Top=�6j   51      52      53      54      55      