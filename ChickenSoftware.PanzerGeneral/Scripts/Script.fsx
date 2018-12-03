
//type TapEventArgs(tileId:int) = 
//          inherit EventArgs()
//          member this.TileId = tileId

//let tapRecognizer = new TapGestureRecognizer()
//let handleTapEvent (sender:Object) (e:TapEventArgs) =
//    //app.MainPage.DisplayAlert(unitName, unitInformation, "OK") |> ignore
//    ()

//let tapEventHandler = new EventHandler<TapEventArgs>(handleTapEvent)
//tapRecognizer.Tapped.AddHandler(tapEventHandler :> EventHandler<EventArgs>)