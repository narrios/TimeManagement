   M S B A M L   `   `   `  ����I  ETimeManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TimeManagementTimeManagement  5   	   &     TimeManagement.expandEventWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:TimeManagement     5   	   _ [MaterialDesignThemes.Wpf, Version=4.4.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Completează sarcină��5	   	   $	��600��6%   $	��400��62      windowStyle#���   6>    � 
ResizeMode$ CanMinimize=�6p    � WindowStartupLocation$ CenterScreen=�6�    � Icon$ /Icons/To-Do_icon.png��6�   .��5
      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      $	��200 �6   �� 5      �� 5      �� 5      �� 5      �� 5      $	��100 �6   .+�5   
   �� $��0��6   $��Denumire sarcină��6!     textBlockStyle#���  69   �� 5   
   $��2��6   $��Conținut sarcină��6!   #���  69   �� 5   
   $��4��6   $��Început sarcină��6!   #���  69   �� 5   
   $��6��6   $��Sfârșit sarcină��6!   #���  69   �� 5   
   -   5         ��Name$
 name��5      $��1��6      textBoxStyle#���  6-   �� 5   
   -   5        $ description��5      $��3��6'   $	��190��64   #���  6A   $	��Top=�6t   �� 5    
    �� Orientation$ 
Horizontal=�6   $��5��6.   $
��25 0q�6;   .+�5!      (  "System.Windows.Controls.DatePicker  -   5          Name$ startDatePicker��5!      $	��210��62    � Language$ ro�6>   )  #MaterialDesignThemes.Wpf.TimePicker  5"      -   5        	  Name$	 startTimePicker��5"   (   $��Center=�6A   $	��100��6\   $��25 0 0 0q�6h   
   	Is24Hours
 . $ ro�6�   5#      �� 5%   
   $ 
Horizontal=�6   $��7��6.   $
��25 0q�6;   .+�5&        -   5        $ endDatePicker��5&      $	��210��60   $ ro�6<   6J     5'      -   5        $	 endTimePicker��5'   (   $��Center=�6?   $	��100��6Z   $��25 0 0 0q�6f   
 . $ ro�6�   6�   5(      � 5*   
   $��8��6    � ColumnDefinitions 5+      �� 5,      �� 5-      $
��auto �6#   �� 5.      $
��auto �6#   .+�50      �� -   5         ��Name$ deleteButton��50       � ToolTip$ Șterge sarcina��6+   $��2��6D     buttonStyle#���  6T   $y�	Collapsed=�6�   $��60��6�   $��0,0,25,0q�6�   .��51      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Delete=�6*   $��40��68   $��40��6D   52      �� 53      -   5        $ 	endButton��53      $' !Marchează sarcina ca finalizată��6(   $��1��6R   #���  6b   $y�	Collapsed=�6�   $��60��6�   $��0,0,25,0q�6�   .��54        $ Clock=�6*   $��40��67   $��40��6C   55      �� 56      -	   5        $ 	addButton��56      $��Adaugă sarcină��6(   $��0��6A   #���  6Q   6�   57      58      59      