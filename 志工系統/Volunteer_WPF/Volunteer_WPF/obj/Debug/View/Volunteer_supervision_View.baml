   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   3    -Volunteer_WPF.View.Volunteer_supervision_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	     � Title$   Volunteer_supervision_View��5   	   $	��615��6,   $
��1020��69   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��51* �6   �� 5      $
��287* �6   .+�5   
   ]� -   5         ]�Name$ 
Dock_Frame��5      ��. $��1,1,0.6,0.6q�6=   $��2��6R   .+�5      �� $
��Left=�6   $	��150��61   $��1q�6>   $
��Left=�6I   .+�5      �� -   5         ��Name$ btn_newsupervision��5      $��新增志工督導��65   $��35��6F   $��5q�6R   $��14(�6]   ���	#FFA7E6DA
�������   ButtonTemplate#���   6�   ��6�   Z� 6�   �� 5      $��姓名��6   $��14(�6&   �� 5      -   5        $��nametext��5      $��30��6*   $��5,0,5,10q�66   �� 5      -   5        $ 
btn_select��5      $��搜尋��6-   $��35��6:   $��5q�6F   $��14(�6Q   ���	#FFA7E6DA
�������#���   6�   ��6v   Z� 6�   5      � 5      $	��584��6   $	��Top=�6    $	��858��68    � RowDefinitions 5      �� 5      $��* �6$   �� 5      $��2* �6$   .+�5      �� $��3��6   $��0��6*   $��Bottom=�67   $��Stretch=�6R   
������ 6p   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dg_supervision��5         
IsReadOnly . $��1q�6E      CanUserAddRows .  $��14(�6g      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      	   CellTemplate	 5      �� 5   "   .R�5    &   �� $��編輯��6-   $��50��6:   ���	#FFA7E6DA
���������6]   Z� 6t   5!   #   5#      5%      �� 5&      $
��Auto��6   $��1��6-   .��5'      �� -   5        
 ��Name$
 wondow_show��5'   !   $
��Left=�66   $��10q�6Q   $��1��6]   $	��Top=�6j   5(      5)      5*      5,      5-      