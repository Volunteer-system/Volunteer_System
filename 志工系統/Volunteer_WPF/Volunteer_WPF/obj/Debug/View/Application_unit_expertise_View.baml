   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   8    2Volunteer_WPF.View.Application_unit_expertise_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	   -   5          � Title$%  Application_unit_expertise_View��5   	   $	��450��61   $	��300��6>   .��5
      � ���	#FFA7E6DA � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��4* �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dg_Expertise��5      $��14(�6)   $
��Left=�67      
IsReadOnly . $	��280��6d   $��
5,70,5.0,0q�6q   $	��Top=�6�   $	��294��6�   $��2��6�      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5      �� $��CB��6'   -   5         d� 	IsChecked 5   1   �� *	Checked+6�   5      5      5      �� 5   
   -   5        $��確認��5      $��30��6   $	��100��6*   $��14(�66   
�����@�
�������$��2��6�   $��24,18.6,169.6,21q�6�   ��6n   Z�  J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 3Q�6#   	   ShadowDepth$	 3Q�62   
   Color$
 	LightGray��6B   5      �� 5    
   -   5        $��取消��5       $��30��6   $	��100��6*   $��14(�66   
�����@�
�������$��2��6�   $��165,18.6,28.6,21q�6�   ��6n   Z�  5!        5"      $ 3Q�6#   $	 3Q�62   $
 	LightGray��6B   5$      �� 5%   
   $-��'設定此運用單位的期望專長，��6   $��14(�6(   $
��Left=�66   $��	10,10,0,0q�6Q   $	��Top=�6d   �� 5&   
   $<��6會於排班作業優先顯示符合專長的志工。��6   $��14(�6-   $
��Left=�6;   $��	10,30,0,0q�6V   $	��Top=�6i   5'      5(      