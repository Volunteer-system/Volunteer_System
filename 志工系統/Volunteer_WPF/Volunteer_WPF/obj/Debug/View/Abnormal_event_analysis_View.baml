   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   5    /Volunteer_WPF.View.Abnormal_event_analysis_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	     � Title$"  Abnormal_event_analysis_View��5   	   $	��650��6.   $
��1020��6;   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��51* �6   �� 5      $
��287* �6   .+�5   
   ]� -   5         ]�Name$ 
Dock_Frame��5      ��. $��1,1,0.6,0.6q�6=   $��2��6R   .+�5      �� $
��Left=�6   $	��150��61   $��1q�6>   $
��Left=�6I   .+�5      �� -   5         ��Name$ btn_application_unit��5      $��運用單位��67   $��35��6F   $��5q�6R   $��14(�6]   ���	#FFA7E6DA
�������   ButtonTemplate#���   6�   ��6�   Z� 6�   �� 5      $	��150��6   $��1q�6(   
�������._�5      �� $��14(�6   $��27��6*   $��	0,0,0.4,0q�66   .��5      �� -   5         ��Name$ txt_unit��5   $   $
��Wrap=�66   5      5      �� 5      $��事件類別��6   $��14(�6(   �� 5      -   5        $��cbb_category��5      $��28��60   $��3q�6<   �� 5      $��事件日期��6   $��14(�6(   (  "System.Windows.Controls.DatePicker  5      -   5        $��dp_startdate��5      �� 5      $��~��6   $��16(�6%     5      -   5        $��
dp_enddate��5      �� 5      -   5        $ btn_selectabnormal_event��5      $��搜尋��6;   $��35��6H   $��5q�6T   $��14(�6_   ���	#FFA7E6DA
�������#���   6�   ��6�   Z� 6�   �� 5      -   5        $ btn_eventlist_excel��5      $��輸出Excel��66   $��35��6H   $��5q�6T   $��14(�6_   ���	#FFA7E6DA
�������#���   6�   ��6�   Z� 6�   5      � 5      $	��610��6   $	��Top=�6    $	��858��68    � RowDefinitions 5       �� 5!      $��* �6$   �� 5"      $��* �6$   .+�5$      �� $
��Auto��6   $��0��6-   .��5%      �� -	   5         ��Name$ wondow_show��5%   !   $
��Left=�66   $��10q�6Q   $��0��6]   $	��Top=�6j   5&      &   System.Windows.Controls.DataGrid  5'      -
   5          Name$ dg_abnormal_event��5'      $��1��66      
IsReadOnly . $��1q�6U   	   CanUserAddRows	 .  $��14(�6w   5(      5)      5*      5+      5,      