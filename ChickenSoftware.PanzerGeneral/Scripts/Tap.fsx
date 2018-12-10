
type TapEventArgs(tileId:int) = 
          inherit EventArgs()
          member this.TileId = tileId

let tapRecognizer = new TapGestureRecognizer()
let handleTapEvent (sender:Object) (e:TapEventArgs) =
    //Figure out how to get the unitName and unitInformation from the event args
    app.MainPage.DisplayAlert(unitName, unitInformation, "OK") |> ignore
    ()

let tapEventHandler = new EventHandler<TapEventArgs>(handleTapEvent)
tapRecognizer.Tapped.AddHandler(tapEventHandler :> EventHandler<EventArgs>)