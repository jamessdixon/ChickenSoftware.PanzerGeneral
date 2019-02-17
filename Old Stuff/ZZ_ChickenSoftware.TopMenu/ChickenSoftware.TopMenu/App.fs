namespace ChickenSoftware.TopMenu

open Xamarin.Forms

type App() =
    inherit Application()
    let grid = Grid(VerticalOptions=LayoutOptions.FillAndExpand, 
                    HorizontalOptions=LayoutOptions.FillAndExpand, 
                    ColumnSpacing=0.0, RowSpacing=0.0,
                    BackgroundColor=Color.Gray)
    let boxView0 = BoxView(BackgroundColor=Color.Red)
    let boxView1 = BoxView(BackgroundColor=Color.White)
    let boxView2 = BoxView(BackgroundColor=Color.Blue)

    let button0 = Button(BackgroundColor=Color.Yellow, Text="Press Me")
    let button1 = Button(BackgroundColor=Color.Green, Text="No, Press Me")

    let stack = StackLayout(VerticalOptions = LayoutOptions.Center)
    let label = Label(XAlign = TextAlignment.Center, Text = "Welcome to F# Xamarin.Forms!")

    do
        stack.Children.Add(label)

        grid.RowDefinitions.Add(RowDefinition(Height=GridLength(50.0,GridUnitType.Absolute)))
        grid.RowDefinitions.Add(RowDefinition(Height=GridLength(100.0,GridUnitType.Absolute)))
        grid.ColumnDefinitions.Add(ColumnDefinition(Width=GridLength(1.0,GridUnitType.Star)))
        grid.ColumnDefinitions.Add(ColumnDefinition(Width=GridLength(1.0,GridUnitType.Star)))

        Grid.SetRow(boxView0,0)
        //Grid.SetRow(boxView1,1)
        Grid.SetRow(button0,1)
        Grid.SetRow(button1,1)
        //Grid.SetRow(boxView2,2)
        Grid.SetRow(stack,2)


        Grid.SetColumnSpan(boxView0, 2)
        //Grid.SetColumnSpan(boxView1, 2)
        //Grid.SetColumnSpan(boxView2, 2)
        Grid.SetColumnSpan(stack,2)

        Grid.SetColumn(button0,0)
        Grid.SetColumn(button1,1)

        grid.Children.Add(boxView0)
        //grid.Children.Add(boxView1)
        //grid.Children.Add(boxView2)
        grid.Children.Add(button0)
        grid.Children.Add(button1)
        grid.Children.Add(stack)

        base.MainPage <- ContentPage(Content = grid)


