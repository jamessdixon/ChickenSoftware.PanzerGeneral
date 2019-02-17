namespace ChickenSoftware.TapExample

open System
open Xamarin.Forms

type App() =
    inherit Application()

    let stack = StackLayout(VerticalOptions = LayoutOptions.Center)
    let label = Label(XAlign = TextAlignment.Center, Text = "Welcome to F# Xamarin.Forms!")
    let image = Image()
    let image2 = Image()

    do
        let tapRecognizer = new TapGestureRecognizer()
        let handleTapEvent (sender:Object) (args:EventArgs) =
            label.Text <- "Tapped at " + DateTime.Now.ToString() 
            ()

        let tapEventHandler = new EventHandler(handleTapEvent)
        tapRecognizer.Tapped.AddHandler(tapEventHandler)
        label.GestureRecognizers.Add(tapRecognizer)

        let doubleTapRecognizer = new TapGestureRecognizer()
        doubleTapRecognizer.NumberOfTapsRequired <- 2
        let handleDoubleTapEvent (sender:Object) (args:EventArgs) =
            label.Text <- "Double Tapped at " + DateTime.Now.ToString() 
            ()

        let doubleTapEventHandler = new EventHandler(handleDoubleTapEvent)
        doubleTapRecognizer.Tapped.AddHandler(doubleTapEventHandler)
        label.GestureRecognizers.Add(doubleTapRecognizer)
        stack.Children.Add(label)

        image.Source <- ImageSource.FromResource("tacmapdry27")
        image.GestureRecognizers.Add(tapRecognizer)
        stack.Children.Add(image)

        image2.Source <- ImageSource.FromResource("tacicons21")
        image2.GestureRecognizers.Add(tapRecognizer)
        stack.Children.Add(image2)
        
        base.MainPage <- ContentPage(Content = stack)



