   M S B A M L   `   `   `  ����J  FWpfFinalProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfFinalProjectWpfFinalProject  5   	       WpfFinalProject.Chat   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfFinalProject     5   	     � Title$
  Chat��5   	   $	��450��6   $	��800��6#    � 	Resources 5	         myHeaderStyle �� 
TargetType �� Property �� Value��%Y   &	        �� 5
   
   " ��6&   .�5      ��  � y�$ 	Collapsed=�6+   5      5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��	ChatView2��5   .   $
��Left=�6   $	��160��6?   $y�Hidden=�6K      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+5      5        5   
   -   5        $��	ImgAdress��5   '   $y�Hidden=�6   6:   �� 5   
   $=��7C:\WpfProject\WpfFinalProject\Background\Background.png��6   $
��Fill=�6Q   6b   �� 5   
   -   5        $	��lst��5      $	��240��6   $	��Top=�6+   $	��600��6C   $��
120,40,0,0q�6O   
������ 
������ $��20(�6�    �� View 5      �� 5      !	 �� ColumnHeaderContainerStyle#	 [  6   
 �� AllowsColumnReorder
 .  .P�5      �� -   5        GridViewColumnName��$
��Name��5   A    �� Width$ 65��6P    �� CellTemplate 5      �� 5      .R�5   "   A� $
��prof��6*   $��40��66   $��40��6B   $��10,0,0,0q�6M   ��5   &   �� 5    *   img�� �� ImageSource 6B   �� *ImageAddress+5"   #   5#      5%      �� 5&      -   5        GridViewColumnTags��$
��Tags��5&   A   $	 500��6O    �� DisplayMemberBinding 6[   �� *	message+5'      5)      �� 5*   
   -   5        $��
txtMessage��5*   W   $��Bottom=�6   $	��200��6-   $��0,0,0,18q�69   $��20��6K   .��6i    5+      ��%^   '��       �� 5,      " ��6   .�5-      ��  �@��CornerRadius$ 5��61   5.      5/      .��6!   50      �� 51   
   -   5        $��Bottom=�51      $��30��6,   $��30��67   $��
260,0,0,15q�6C   
����  �$��>��6i   
������� 52      ��%`   '��       �� 53      " ��6   .�54      ��  �@��CornerRadius$	 100��65   55      56      57      �� 59   
   -   5        $��ChatView��59   .   $
��Left=�6   $	��160��6>    5:      �� 5;      #	 [  6   
 .  -	   5        	GridView2��.P�5<      �� -
   5        GridViewImage��$
��Name��5<   <   $ 65��6K    5=      �� 5>      .R�5?   "   A� $��pro2f��6*   $��40��67   $��40��6C   ��5@   &   �� 5A   *   Userimg�� 6F   �� *UsersImageAddress+5C   #   5D      5F      �� 5G      -   5        UserName��$
��Tags��5G   7    6E   �� *	UserNames+5H      5J      5K      5L      