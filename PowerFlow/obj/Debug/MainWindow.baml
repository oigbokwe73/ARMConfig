   M S B A M L   `   `   `  ����  	PowerFlow    PowerFlow.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    � WindowStyle$
 None=�65    � AllowsTransparency . 
������  � 
ResizeMode$ CanResizeWithGrip=�6{      BorderlessResizableWindow#���   6�   .��5      �� $��.5q�6   
�����p�$��5q�6:   ���	#FFEEEEF2 J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6      Opacity$
 0.15Q�6/   ._�5	   
   � .+�5
      � -   5         �Name$ grid_titlebar��5
      $��25��6*   $	��Top=�66   .+�5      �� $��Right=�6    �� Orientation$ 
Horizontal=�69   .+�5      �� -   5        	 ��Name$	 btn_minimize��5      
������ $��0q�6L   $��_��6`   $��25��6l   �� 5      -   5        $	 btn_maximize��5      
������ $��0q�6L   $
��[  ]��6`   $��25��6o   �� 5      -   5        $	 	btn_close��5      
������ $��0q�6I   $��x��6]   $��25��6i   5      �� 5      $
��Left=�6   $ 
Horizontal=�68   .+�5      �� -   5        
 ��Name$
 img_windowlogo��5      �� 5      -   5         ��Name$ lbl_windowtitle��5      $,��&PowerFlow - the ultimate workflow tool��65   5      5      � 5      -   5        $ grid_contentarea��5      $��	0,25,0,20q�6-   .+�5      o� $	��Top=�6   
������ .@�5      m� -   5        $	��Run��5      6,   m� 5      $
��File��6   m� 5      $
��Edit��6   m� 5      $
��VIEW��6   m� 5      $��TOOLS��6   m� 5      $
��HELP��6   5      � 5      -	   5        $ grid_designsurface��5      $��2,18,2,0q�63    � ColumnDefinitions 5      �� 5       $
��Auto �6+   �� 5!      $��* �6+   �� 5"      $
��Auto �6+   5$      5&      � 5'      -
   5        $ grid_statusbar��5'      $��Bottom=�6+   $��20��6F   ���	#FF007ACC6k   5)      5*      5+      