   M S B A M L   `   `   `  ����   Gestures    Gestures.Drawing3D   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  WPF 3D Test��$	��400��$	��400�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � WindowStyle$
 None=� � Cursor$
 None�� � ForceCursor .  � 	Resources    NavigationButton �� 
TargetType	 �� Property
 �� Value �� 
TargetType �� Triggers P� Property P� Value��%�   &	        �� " ��.��� 	 � ��
 �	#FF4F4F4F�� 	 � ��
 �� " ��.R��� #��z�����	#FF4F4F4F$��1q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� 	 � ��
 �#585858.��� -   
���   �$
 None�� .  � RowDefinitions �� $
��Auto ��� $��* � � ColumnDefinitions �� $��* �.+��  �� $
��Auto ��� $��* � �� $��* ��� $��* �.+��� -   $��Wczytaj model��#��[  $��0��$��0��$	��100��$��50��$��0,10,0,0q����#ffffff�� -   $��Zakończ��#��[  $��0��$��1��$	��100��$��50��$��0,10,0,0q����#ffffff'� -    '�Name$ viewport��$��1��$��0�� '� Camera =� -   
camera�� � FarPlaneDistance$ 50Q� � LookDirection$ 0,0,-105� � UpDirection$ 0,1,05� � NearPlaneDistance$ 0Q� � Position$ 0,0,81� =� FieldOfView$ 90Q�.x�g� -   	model�� g� Content h� -   	group��.����  �� Color$ DarkGray��"� -	   	light��$ White�� !� Position$ 2,5,101�