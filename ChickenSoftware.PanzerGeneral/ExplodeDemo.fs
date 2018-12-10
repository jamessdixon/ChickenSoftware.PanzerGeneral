module ChickenSoftware.PanzerGeneral.ExplodeDemo

open Xamarin.Forms

let addContent (layout:AbsoluteLayout) =
    let image = new Image()
    image.Source <- ImageSource.FromResource("explode0")
    let x = 50.0 
    let y = 50.0
    let height = 50.0 
    let width = 60.0 
    let rectangle = new Rectangle(x,y,width,height)
    layout.Children.Add(image , rectangle)

    let mutable index = 1
    let callback = new System.Func<bool>(fun _ -> 
        match index with 
        | 1 -> image.Source <- ImageSource.FromResource("explode1"); index <- 2
        | 2 -> image.Source <- ImageSource.FromResource("explode2"); index <- 3
        | 3 -> image.Source <- ImageSource.FromResource("explode3"); index <- 4
        | 4 -> image.Source <- ImageSource.FromResource("explode4"); index <- 5
        | 5 -> image.Source <- ImageSource.FromResource("explode0"); index <- 99
        | _ -> ()
        true)
    Device.StartTimer(System.TimeSpan.FromSeconds(0.25),callback)

let populateImage =
    let layout = new AbsoluteLayout()
    layout.HeightRequest <- 5000.0
    layout.WidthRequest <- 5000.0
    addContent layout
    let scrollView = new ScrollView()
    scrollView.Content <- layout
    scrollView

type App() =
    inherit Application()

    do
        let scrollView = populateImage
        base.MainPage <- ContentPage(Content = scrollView)

