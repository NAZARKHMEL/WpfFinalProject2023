   M S B A M L   `   `   `  ����J  FWpfFinalProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfFinalProjectWpfFinalProject  5   	   !    WpfFinalProject.ProductView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfFinalProject     5   	     � Title$  ProductView��5   	   $	��450��6   $	��800��6*   .��5	      � 
�������.+�5
   
   �� -   5        $��ProdAddress��5
   $   $y�Hidden=�6   69   �� 5   
   -   5        $��ProfImg��5   $   $y�Hidden=�6   65   �� 5   
   .��5      �� $	��550��6   .+�5      �� 
�������$	��550��6,   $	��250��68   $��0,40,0,0q�6E   ._�5      �� $	��200��6V   $	��400��6d   ��6   ��  �� ElementName$ ProdAddress�� �� Path$ Content�6r   5      �� 5      
�������$	��550��6,   $	��180��68   $��0,20,0,0q�6E   ._�5      �� .+�5      �� -   5        $��ProdName��5       $��20(�60   6@   �� 5      -   5        $��	ProdPrice��5       $
��Bold"�61   $��30(�6C   6S   �� 5      $��Description��6    $
��Bold"�66   $��20(�6H   6X   �� 5      -   5        $��ProdDescripe��5       66   5      5      �� 5      
�������$	��550��6,   $	��120��68   $��0,20,0,0q�6E   ._�5      �� .+�5      �� $��Seller��6    $
��Bold"�61   $��20(�6C   6S   �� 5       �� Orientation$ 
Horizontal=�6%   .+�5      A� -   5        $
��prof��5   &   $��50��62   $��50��6>   $��10,0,0,0q�6I   ��5   "   �� 5    &   -   5        img�� �� ImageSource 5    >   �� $ ProfImg��$ Content�5"      �� 5#      -   5        $��SellerLogin��5#   $   $��20(�67   6G   �� 5$      -	   5        $��Send message��5$   %   $��	250,0,0,0q�6<   $	��150��6O   $��20(�6[   
����  �
������� � 	Resources 5%   "    �� 
TargetType �� Property �� Value��%^   '��       �� 5&   &   " ��6,   .�5'   *   ��  �@��CornerRadius$ 6��6I   5(   '   5)   #   5*      5+      5,      5-      5.      5/      &   System.Windows.Controls.DataGrid  50   
   -
   5        $��ProfInfo��50      $y�Hidden=�6#   69   51      52      