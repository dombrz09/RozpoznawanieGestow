   M S B A M L   `   `   `  ����C  ?Gestures, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GesturesGestures  5   	       Gestures.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   (localclr-namespace:Gestures     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     6.   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6k     � Title$  
MainWindow��5   	   $��331.067��6   $��335.076��6-   ���#000000 � 	Resources 5         
MenuButton �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value��%�  &	        �� 5   
   " ��6#   .�5	      ��  � �� �	#FF4F4F4F6+   �� 5
       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   ���	#FF4F4F4F$��1q�6c   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5       5      P� 5       �@J�IsMouseOver . . �5      ��  � �� �#58585863   5      5      5      .��5      (� .��5   
   � $	��200��6   $	��164��6   	 � RowDefinitions	 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      $
��Auto �6    �� 5"      $
��Auto �6    
 � ColumnDefinitions
 5$      �� 5%      �� 5&      $��0* �6#   .+�5(      �� -   5         ��Name$ ButtonLoadPDF��5(   V   $��0��6   $��0��6"   #��[  62   $��Wczytaj .pdf��6m   $��Center=�6�   $��
44,10,40,1q�6�   $	��116��6�   ���#ffffff6�   �� 5)      -   5        $��1��5)      $��0��6"   #��[  62   $��Rysowanie 3D��6V   $��Center=�6m   $��
44,11,40,0q�6�   $	��116��6�   ���#ffffff6�   �� 5*      -   5        $��2��5*      $��0��6"   #��[  62   $��AdobeReader��6V   $��Center=�6l   $��
44,10,40,1q�6�   $	��116��6�   ���#ffffff6�   �� 5+      -   5        $��3��5+      $��0��6"   #��[  62   $��
PowerPoint��6V   $��Center=�6k   $��
44,11,40,0q�6�   $	��116��6�   ���#ffffff6�   �� 5,      -   5        $��Spinner��5,      $y�Hidden=�6#   $��
loader.gif��67    J� RenderTransformOrigin$ 0.5, 0.5&�6K   $��69,0,67,-43q�6l   $��4��6�   $��24��6�   $��Bottom=�6�    � Triggers 5-      :� 5.       :� RoutedEvent$ FrameworkElement.Loaded��6#   [�5/      �� 50      .��51   "   ��  �� 
TargetName$ Spinner��6-    �� TargetProperty$- 'RenderTransform.(RotateTransform.Angle)�6M   .��52   &   W�  W� From$ 0X�66   	 W� To$	 360X�6?    g� 	BeginTime$ 0:0:0X�6H    g� Duration$ 0:0:2D�6Z    g� RepeatBehavior$ Forever��6k   53   #   54      56      z�58      �� 59       �� Angle$ 0Q�6&   ��5;      �� 5<      " ��6    5=      �� 5>       �� Binding 6*   �� *IsBusy+ �� Value	 False6E   .a�5?   "   ��  � y�$ Hidden=�6?   5@      5B      5D      5E      5F      5G      