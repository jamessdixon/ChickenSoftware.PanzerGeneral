
#r "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/packages/Xamarin.Forms.3.0.0.482510/lib/netstandard2.0/Xamarin.Forms.Core.dll"
#r "/Users/jamesdixon/GitHub/chickensoftware.xf/Droid/obj/Debug/linksrc/netstandard.dll"

open Xamarin.Forms
open Xamarin.Forms.Xaml

type MainPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<MainPage>)

    let addContent (layout:AbsoluteLayout) =
        let image = new Image()
        image.Source <- ImageSource.FromResource("explode_0_0")
        let x = 50.0 
        let y = 50.0
        let height = 50.0 
        let width = 60.0 
        let rectangle = new Rectangle(x,y,width,height)
        layout.Children.Add(image , rectangle)

        let mutable index = 1
        let callback = new System.Func<bool>(fun _ -> 
            match index with 
            | 1 -> image.Source <- ImageSource.FromResource("explode_1_0"); index <- 2
            | 2 -> image.Source <- ImageSource.FromResource("explode_2_0"); index <- 3
            | 3 -> image.Source <- ImageSource.FromResource("explode_3_0"); index <- 4
            | 4 -> image.Source <- ImageSource.FromResource("explode_4_0"); index <- 5
            | 5 -> image.Source <- ImageSource.FromResource("explode_5_0"); index <- 99
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
        base.Content <- scrollView