   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   .    (Volunteer_WPF.View.Application_unit_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	     � Title$  Application_unit_View��5   	   $	��615��6'   $
��1020��64   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��51* �6   �� 5      $
��287* �6   .+�5   
   ]� -   5         ]�Name$ 
Dock_Frame��5      ��. $��1,1,0.6,0.6q�6=   $��2��6R   .+�5      �� $
��Left=�6   $	��150��61   $��1q�6>   $
��Left=�6I   .+�5      �� -   5         ��Name$ btn_newApplication_unit��5      $��新增運用單位��6:   $��35��6K   $��5q�6W   $��14(�6b   ���	#FFA7E6DA
�������   ButtonTemplate#���   6�   ��6�   Z� 6�   �� 5      $��單位名稱��6   $��14(�6(   $��10,0,0,0q�66   �� 5      -   5        $��nametext��5      $��30��6*   $��5,0,5,10q�66   �� 5      $��組別��6   $��14(�6&   $��10,0,0,0q�64   �� 5      -   5        $��	cbb_group��5      $��1��6,   $��28��69   $��3q�6E   �� 5      -   5        $  btn_selectApplication_unit��5      $��搜尋��6=   $��35��6J   $��5q�6V   $��14(�6a   ���	#FFA7E6DA
�������#���   6�   ��6�   Z� 6�   9� 5      $��志工總人數��6   $��0,2,0,0q�6+   $��14(�6<   .��5      �� .+�5      	� -   5        $��
members_10��5   &    	� 	GroupName$ members��68   $��1~10人��6L   	� 5      -   5        $��
members_20��5   &   $ members��68   $��11~20人��6L   	� 5      -   5        $��
members_30��5   &   $ members��68   $��21~30人��6L   	� 5      -	   5        $��
members_40��5   &   $ members��68   $��31~40人��6L   	� 5      -
   5        $��members_40up��5   &   $ members��6:   $��41人以上��6N   5      5      5      � 5       $	��584��6   $	��Top=�6    $	��858��68    � RowDefinitions 5!      �� 5"      $��* �6$   �� 5#      $��2* �6$   .+�5%      �� $��3��6   $��0��6*   $��Bottom=�67   $��Stretch=�6R   
������ 6p   &   System.Windows.Controls.DataGrid  5&      -   5          Name$ dg_Application_unit��5&      $��1q�69      
IsReadOnly . $��14(�6V      Columns 5'      4  .System.Windows.Controls.DataGridTemplateColumn  5(      	   CellTemplate	 5)      �� 5*   "   .R�5+   &   �� -   5        $��修改��5+   -   $��50��6:   ���	#FFA7E6DA
���������6r   Z� 6�   5,   #   5.      50      �� 51      $
��Auto��6   $��1��6-   .��52      �� -   5        
 ��Name$
 wondow_show��52   !   $
��Left=�66   $��10q�6Q   $��1��6]   $	��Top=�6j   53      54      55      56      57      