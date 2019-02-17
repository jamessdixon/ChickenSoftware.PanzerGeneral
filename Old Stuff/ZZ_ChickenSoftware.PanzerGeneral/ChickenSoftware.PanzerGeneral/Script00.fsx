        //let boxView = new BoxView()
        //boxView.Color <- Color.Red
        ////boxView.WidthRequest <- 10.0
        ////boxView.HeightRequest <- 10.0
        //boxView.VerticalOptions <- LayoutOptions.Center
        //boxView.HorizontalOptions <- LayoutOptions.Center
        //let rectangle = new Rectangle(10.0,50.0,60.0,50.0)
        //let layout = new AbsoluteLayout()
        //layout.WidthRequest <- 600.0
        //layout.HeightRequest <- 600.0

        //[0.0 .. 100.0]
        //|> Seq.iter(fun i -> createBox i layout)

        //let anotherBoxView = new BoxView()
        //anotherBoxView.Color <- Color.Blue
        //anotherBoxView.VerticalOptions <- LayoutOptions.Center
        //anotherBoxView.HorizontalOptions <- LayoutOptions.Center
        //let anotherRectangle = new Rectangle(100.0,100.0,60.0,50.0)

        //layout.Children.Add(boxView,rectangle)
        //layout.Children.Add(anotherBoxView,anotherRectangle)

        //let stackLayout = new StackLayout()
        //let boxView = new BoxView();
        //boxView.BackgroundColor <- Color.Red
        //boxView.HeightRequest <- 600.0
        //boxView.WidthRequest <- 150.0
        //let entry = new Entry()
        //stackLayout.Children.Add(boxView)
        //stackLayout.Children.Add(entry)

        //let scrollView = new ScrollView()
        //scrollView.Content <- scrollView

        //This works
        //let layout = new StackLayout()
        //[0..100]
        //|> Seq.iter(fun i -> layout.Children.Add(createButton i))
        //let scrollView = new ScrollView()
        //scrollView.Content <- layout
        //base.Content <- scrollView

        //let layout = new AbsoluteLayout()
        //layout.HeightRequest <- 5000.0
        //layout.WidthRequest <- 5000.0
        //[0..100]
        //|> Seq.iter(fun i -> layout.Children.Add(createButton i, createRectangle i))
        //let scrollView = new ScrollView()
        //scrollView.Content <- layout
        //base.Content <- scrollView

    //let createBox i (layout:AbsoluteLayout)=
    //    let boxView = new BoxView()
    //    match i%2.0 with 
    //    | 0.0 -> boxView.Color <- Color.Red
    //    | _ -> boxView.Color <- Color.Blue
    //    boxView.VerticalOptions <- LayoutOptions.Center
    //    boxView.HorizontalOptions <- LayoutOptions.Center
    //    let rectangle = new Rectangle(10.0*i,50.0*i,60.0,50.0)
    //    layout.Children.Add(boxView,rectangle)

    //let createButton i =
    //    let button = new Button()
    //    button.Text <- "Button:" + i.ToString()
    //    button

