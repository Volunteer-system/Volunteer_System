   M S B A M L   `   `   `  ����H  DVolunteer_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Volunteer_WPF.ViewVolunteer_WPF.View  5   	       Volunteer_WPF.View.Login   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Volunteer_WPF.View     5   	     � Title$  Login��5   	   $	��450��6   $	��800��6$    � 	Resources 5	         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1 �� 
TargetType �� 
TargetType	 ��Name
 �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  TextBox.Static.Border  TextBox.MouseOver.Border  TextBox.Focus.Border  TextBoxStyle1-  'System.Windows.Input.KeyboardNavigation ��Name �� Triggers ]� 
Conditions �� Property �� Value��%�  &	        &	 �     &	 �     &	 �     &	     &	 6    &	 Y    &	 |    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7 	 &	 �    &	 �    &	 �    &	     7  7  7  �� 5
   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6%   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5       � ��8  6,   �� 5        � ��# � ��6+   �� 5!       � ��$ 1q�60   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�69   �� 5$       � ��$ 1q�6(   �� 5%       � �� 5&      �� 5'      " ��6&   .R�5(      �� $	 border��6!   $
 5��61   #��z��6B   #��z��6n   #��z��6�    . ._�5)      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5*       5+      P� 5,       �@��IsDefaulted . . �5-   "   �� $ border��6@   # � ��6T    � ��5.      P� 5/       �@J�IsMouseOver . . �50   "   �� $ border��6?   8   � ��6S   �� 51   "   $ border��6@   8   � ��6T   52      P� 53       � �� . . �54   "   �� $ border��6?   8   � ��6S   �� 55   "   $ border��6@   8   � ��6T   56      P� 57       � {� .  . �58   "   �� $ border��6?   8   � ��6S   �� 59   "   $ border��6@   8   � ��6T   �� 5:   "   $ contentPresenter��6K   8 	  � ��6i   5;      5=      5?      5@      �� 5A   
   $ 	#FFABAdB3��68   �� 5B   
   $ 	#FF7EB4EA��6;   �� 5C   
   $ 	#FF569DE5��67   �� 5D   
   " ��6&   .�5E      ��  � ��# � ��6+   �� 5F       � ��8   6,   �� 5G       � ��# � ��6+   �� 5H       � ��$ 1q�60   �� 5I       �@ TabNavigation$
 None=�6A   �� 5J       � ��$
 Left=�6;   �� 5K       �@�FocusVisualStyle 61   Z� �� 5L       �@J�	AllowDrop . 6*   �� 5M       �@��PanningMode$ VerticalFirst=�69   �� 5N       �@��IsFlicksEnabled .  67   �� 5O       � �� 5P      �� 5Q      " ��6&   .R�5R      �� $	 border��6!   $
 3��61   #��z��6B   #��z��6n   #��z��6�    . ._�5S      �� $ PART_ContentHost��6+   |�.  $��Hidden=�6W   $��Hidden=�6~   5T       5U      P� 5V       � {� .  . �5W   "   �� $ border��6<    �@J�Opacity$
 0.56Q�6P   5X      P� 5Y       �@J�IsMouseOver . . �5Z   "   �� $ border��6@   8   � ��6T   5[      P� 5\       �@J�IsKeyboardFocused . . �5]   "   �� $ border��6@   8   � ��6T   5^      5`      5b       5c      ]� 5d       5e      �� 5f      + �@��#IsInactiveSelectionHighlightEnabled . 6S   �� 5g       �@��IsSelectionActive .  6A   ./�5i      ��  �@��SelectionBrush# � /�67   5j      5l      5m      .��5n      � .+�5o   
   �� $��Center=�6   $	��250��61   $��145,106,147,63q�6>   $��Center=�6V   $	��500��6q    �� RadiusX$ 10��6}    �� RadiusY$ 10��6�   ��5p      �� 5q       �� EndPoint$ 0.6,1&�6&    �� 
StartPoint$ 0.6,0&�67   .��5r      �  � Color$ 	#FFA7E6DA��6#    � Offset$ 1Q�65   � 5s      $ 	#FFCDF5EE��6#   5t      5v      �� 5w   
   -   5        $��登入系統��5w      $
��Left=�6    $��297,253,0,0q�6;   $	��Top=�6P   $	��200��6h   $��30��6t   
�����@�
�������$��18(�6�   $
��Bold"�6�   #��� 
 6�   ��6�   Z�  J� Effect 5x      3  -System.Windows.Media.Effects.DropShadowEffect  5y         
BlurRadius$ 3Q�6#      ShadowDepth$ 3Q�62      Color$ 	LightGray��6B   5{      �� 5|   
   $��帳號��6   $
��Left=�6   $��295,148,0,0q�68   $	��Top=�6M   
�������$��20(�6{   $
��Bold"�6�   �� 5}   
   $��密碼��6   $
��Left=�6   $��295,198,0,0q�68   $	��Top=�6M   
�������$��20(�6{   $
��Bold"�6�   �� 5~   
   -   5         ��Name$ Account_number_TextBox��5~      $
��Left=�62   $��30��6M   $��345,151,0,0q�6Y     �� TextWrapping$
  Wrap=�6n   $	��Top=�6�   $	��152��6�   #���  6�   ��6�   Z� �� 5   
   -   5        $ Password_TextBox��5      $
��Left=�6,   $��30��6G   $��345,201,0,0q�6S   $
  Wrap=�6h   $	��Top=�6|   $	��152��6�   #���  6�   ��6�   Z� �� 5�   
   $��忘記密碼?��6   
�������$��14(�63   $
��Left=�6A   $��361,288,0,0q�6\   $	��Top=�6q   �� 5�   
   $
��Left=�6   $��95��6+   $��
182,32,0,0q�67   $	��Top=�6K   $��98��6c   $s��mC:\Users\peter.wu\Documents\GitHub\Volunteer_System\志工系統\Volunteer_WPF\Volunteer_WPF\image\person.png��6n   5�      5�      