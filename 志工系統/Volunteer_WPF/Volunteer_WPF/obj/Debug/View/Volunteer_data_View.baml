   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	   -clr-namespace:Volunteer_WPFVolunteer_WPF  5   	   ,    &Volunteer_WPF.View.Volunteer_data_View   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	   5Volunteer_WPFclr-namespace:Volunteer_WPF     5   	   -   5          � Title$  Window1��5
   	   $	��615��6   $
��1020��6&   .��5      � ���	#FFA7E6DA.+�5   
   �� $��中文姓名��6   $
��Left=�6   $��	20,20,0,0q�6:   $	��Top=�6M   �� 5   
   -   5        $��txtChineseName��5      $
��Left=�6(   $��23��6C   $��
150,20,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $�� ��6w   $	��Top=�6   $	��120��6�   6�   �� 5   
   $��英文性名��6   $
��Left=�6   $��	20,50,0,0q�6:   $	��Top=�6M   �� 5   
   -   5        $��txtEnglishName��5      $
��Left=�6(   $��23��6C   $��
150,50,0,0q�6O   $
 Wrap=�6c   $�� ��6w   $	��Top=�6   $	��120��6�   6�   �� 5   
   $��姓別��6   $
��Left=�6   $��	20,80,0,0q�68   $	��Top=�6K   �� 5   
   -   5        $��cbsex��5      $
��Left=�6    $��
150,80,0,0q�6;   $	��Top=�6O   $	��120��6g   5      �� 5   
   $��生日��6   $
��Left=�6   $��
20,110,0,0q�68   $	��Top=�6L   �� 5   
   -   5        $��cbBirthDayYear��5      $
��Left=�6*   $��150,110,0,0q�6E   $	��Top=�6Z   $��60��6r   �� 5   
   -   5        $��cbBirthDayMonth��5      $
��Left=�6*   $��210,110,0,0q�6E   $	��Top=�6Z   $��60��6r   �� 5   
   -   5        $��cbBirthDayDay��5      $
��Left=�6(   $��270,110,0,0q�6C   $	��Top=�6X   $��60��6p   �� 5   
   $#��身分證字號(護照編號)��6   $
��Left=�6&   $��
20,140,0,0q�6A   $	��Top=�6U   �� 5    
   -   5        $��txtIDcrad_no��5       $
��Left=�6&   $��23��6A   $��150,140,0,0q�6M   $
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��120��6�   �� 5"   
   $��	病歷號��6   $
��Left=�6   $��
20,170,0,0q�69   $	��Top=�6M   �� 5#   
   -	   5        $��txtMedical_record_no��5#      $
��Left=�6.   $��23��6I   $��150,170,0,0q�6U   $
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��120��6�   �� 5%   
   $��身份類別��6   $
��Left=�6   $��
20,200,0,0q�6:   $	��Top=�6N   �� 5&   
   -
   5         ��Name$ cbIdentity_type��5&      $
��Left=�6,   $��150,200,0,0q�6G   $	��Top=�6\   $	��120��6t   �� 5(   
   $��服務年資��6   $
��Left=�6   $��
20,230,0,0q�6:   $	��Top=�6N   �� 5)   
   -   5        $��txtSeniority��5)      $
��Left=�6&   $��23��6A   $��150,230,0,0q�6M   $
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��120��6�   �� 5+   
   $��加入日期��6   $
��Left=�6   $��
20,260,0,0q�6:   $	��Top=�6N   �� 5,   
   -   5        $ cbJoin_dateYear��5,      $
��Left=�6,   $��150,260,0,0q�6G   $	��Top=�6\   $��60��6t   �� 5-   
   -   5        $ cbJoin_dateMonth��5-      $
��Left=�6-   $��210,260,0,0q�6H   $	��Top=�6]   $��60��6u   �� 5.   
   -   5        $ cbJoin_dateDay��5.      $
��Left=�6+   $��270,260,0,0q�6F   $	��Top=�6[   $��60��6s   �� 50   
   $��離開日期��6   $
��Left=�6   $��
20,290,0,0q�6:   $	��Top=�6N   �� 51   
   -   5        $ cbLeave_dateYear��51      $
��Left=�6-   $��150,290,0,0q�6H   $	��Top=�6]   $��60��6u   �� 52   
   -   5        $ cbLeave_dateMonth��52      $
��Left=�6.   $��210,290,0,0q�6I   $	��Top=�6^   $��60��6v   �� 53   
   -   5        $ cbLeave_dateDay��53      $
��Left=�6,   $��270,290,0,0q�6G   $	��Top=�6\   $��60��6t   �� 55   
   $��離開原因��6   $
��Left=�6   $��
20,320,0,0q�6:   $	��Top=�6N   �� 56   
   -   5        $��cbLeaveReason��56      $
��Left=�6(   $��150,320,0,0q�6C   $	��Top=�6X   $	��120��6p   .@�57      �� $��因病��6   �� 58      $��工作因素��6   �� 59      $��個人因素��6   �� 5:      $��	不適任��6   �� 5;      $��家庭因素��6   5<      �� 5=   
   $��地址��6   $
��Left=�6   $��
20,350,0,0q�68   $	��Top=�6L   �� 5>   
   -   5        $��
txtAddress��5>      $
��Left=�6$   $��23��6?   $��150,350,0,0q�6K   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��460��6�   6�   �� 5?   
   $��電話號碼��6   $
��Left=�6   $��
380,20,0,0q�6:   $	��Top=�6N    J� RenderTransformOrigin$ 
0.41,0.153&�6f   �� 5@   
   -   5        $��txtPhone_no��5@      $
��Left=�6%   $��23��6@   $��
490,20,0,0q�6L   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��120��6�   6�   �� 5A   
   $��手機號碼��6   $
��Left=�6   $��
380,50,0,0q�6:   $	��Top=�6N   �� 5B   
   -   5        $��txtMobile_no��5B      $
��Left=�6&   $��23��6A   $��
490,50,0,0q�6M   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��120��6�   6�   �� 5C   
   $��志工背心號碼��6   $
��Left=�6!   $��
380,80,0,0q�6<   $	��Top=�6P   �� 5D   
   -   5        $��
txtVest_no��5D      $
��Left=�6$   $��23��6?   $��
490,80,0,0q�6K   $
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��120��6�   6�   �� 5E   
   $��郵遞區號��6   $
��Left=�6   $��380,110,0,0q�6:   $	��Top=�6O   �� 5F   
   -   5        $��txtPostal_code��5F      $
��Left=�6(   $��23��6C   $��490,110,0,0q�6O   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $	��120��6�   6�   �� 5H   
   $��學歷��6   $
��Left=�6   $��380,140,0,0q�68   $	��Top=�6M   �� 5I   
   -   5        $��cbEducation��5I      $
��Left=�6&   $��490,140,0,0q�6A   $	��Top=�6V   $	��120��6n   .@�5J      �� $��小學��6   �� 5K      $��國中��6   �� 5L      $��	高中職��6   �� 5M      $��專科��6   �� 5N      $��大學��6   �� 5O      $��	研究所��6   5P      �� 5Q   
   $��發證單位��6   $
��Left=�6   $��380,170,0,0q�6:   $	��Top=�6O   �� 5R   
   -   5        $��txtLssuing_unit_no��5R      $
��Left=�6,   $��23��6G   $��490,170,0,0q�6S   $
 Wrap=�6h   $	��Top=�6|   $	��120��6�   6�   �� 5S   
   $��志工服務手冊編號��6   $
��Left=�6#   $��380,200,0,0q�6>   $	��Top=�6S   �� 5T   
   -   5        $��txtService_manual_no��5T      $
��Left=�6.   $��23��6I   $��490,200,0,0q�6U   $
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��120��6�   6�   �� 5U   
   $��人格量表結果��6   $
��Left=�6!   $��380,230,0,0q�6<   $	��Top=�6Q   �� 5V   
   -   5        $��txtPersonality_scale��5V      $
��Left=�6.   $��23��6I   $��490,230,0,0q�6U   $
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��120��6�   �� 5Z   
   $��志工專長��6   $
��Left=�6   $��380,260,0,0q�6:   $	��Top=�6O   $ 0.681,-0.039&�6g   �� 5[   
   -   5        $��btnSelectExpert��5[      $��18(�6?   $
��Bold"�6M      ButtonStyle1#���   6_   
�����@�
�������$��	選專長��6�   $
��Left=�6�   $��490,260,0,0q�6�   $	��Top=�6�   $��23��6  $	��120��6  ��6(   Z�  J� Effect 5\      3  -System.Windows.Media.Effects.DropShadowEffect  5]         
BlurRadius$ 3Q�6#      ShadowDepth$ 3Q�62      Color$ 	LightGray��6B   5_      �� 5a   
   $��志工組別��6   $
��Left=�6   $��380,290,0,0q�6:   $	��Top=�6O   �� 5b   
   -   5         ��Name$ btnSelectService_Group��5b      $��18(�6H   $
��Bold"�6V   #���   6h   
�����@�
�������$��選服務類別��6�   $
��Left=�6�   $��490,290,0,0q�6�   $	��Top=�6�   $��23��6  $	��120��6  ��61   Z�  5c        5d      $ 3Q�6#   $ 3Q�62   $ 	LightGray��6B   5f      �� 5h   
   $��照片��6   $
��Left=�6   $��
670,20,0,0q�68   $	��Top=�6L   � 5i   
   $��711,20,31.6,364.6q�6   ���	#FFCAFCED.+�5j      �� -   5        $��myImage��5j      5k      �� 5m   
   -   5        $ btnOpen��5m      $��18(�69   $
��Bold"�6G   #���   6Y   
�����@�
�������$��Open...��6�   $
��Left=�6�   $��711,227,0,0q�6�   $	��Top=�6  $��90��6  $��50��6)  ��6"   Z�  5n        5o      $ 3Q�6#   $ 3Q�62   $ 	LightGray��6B   5q      �� 5s   
   -    5        $��btn_confirm��5s      $��18(�6<   $
��Bold"�6J   #���   6\   
�����@�
�������$��確定��6�   $
��Left=�6�   $��
20,400,0,0q�6�   $	��Top=�6�   $��90��6  $��50��6  ��6%   Z�  5t        5u      $ 3Q�6#   $ 3Q�62   $ 	LightGray��6B   5w      �� 5y   
   -!   5        $��	BtnCancel��5y      $��18(�6Q   $
��Bold"�6_   #���   6q   
�����@�
�������$��90��6�   $��50��6�   $��取消��6�   $
��Left=�6�   $��120,400,0,0q�6  $	��Top=�6  ��6:   Z�  5z        5{      $ 3Q�6#   $ 3Q�62   $ 	LightGray��6B   5}      �� 5~   
   -"   5        $��btnDEMO��5~      $��18(�67   $
��Bold"�6E   #���   6W   
�����@�
�������$
��demo��6�   $
��Left=�6�   $��
20,470,0,0q�6�   $	��Top=�6�   $��90��6�   $��25��6	  ��6    Z�  5        5�      $ 3Q�6#   $ 3Q�62   $ 	LightGray��6B   5�      5�      5�      