//namespace ChickenSoftware.PanzerGeneral

//open Xamarin.Forms

//type App() =
    //inherit Application()

    //let createImage path =
    //    let image = new Image()
    //    image.Source <- ImageSource.FromResource(path)
    //    image

    //let createRectangle columnIndex rowIndex =
    //    let xOffset = (((float) columnIndex) * -30.0) + -30.0
    //    let yOffset = 
    //        match columnIndex % 2 = 0 with
    //        | true -> 0.0
    //        | false -> 25.0     
    //    let height = 50.0 
    //    let width = 60.0 
    //    let x = xOffset + ((float) columnIndex) * width
    //    let y = yOffset + ((float) rowIndex) * height
    //    let rectangle = new Rectangle(x,y,width,height)
    //    rectangle

    //let addContent (layout:AbsoluteLayout) c =
    //    layout.Children.Add(createImage "tacmap_dry_139", createRectangle (fst c) (snd c))

    //do
        //let layout = new AbsoluteLayout()
        //layout.HeightRequest <- 5000.0
        //layout.WidthRequest <- 5000.0
        ////1st number is column (x axis)
        ////2nd number is row (y axis)
        //let coordinates = [0,0;1,0;2,0;3,0;4,0;
        //                   0,1;1,1;2,1;3,1;4,1;
        //                   0,2;1,2;2,2;3,2;4,2;
        //                   0,3;1,3;2,3;3,3;4,3;
        //                      ]
        //coordinates
        //|> Seq.iter(fun c -> addContent layout c)
        //let scrollView = new ScrollView()
        //scrollView.Content <- layout
        //base.MainPage <- ContentPage(Content = scrollView)  
