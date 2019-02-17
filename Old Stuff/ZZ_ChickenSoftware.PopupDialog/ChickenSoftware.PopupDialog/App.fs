namespace ChickenSoftware.PopupDialog

open Xamarin.Forms

//https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/pop-ups

type App() as app=
    inherit Application()

    let button_Clicked args =
        let unitName = "Unit 01"
        let unitInformation =
            "Strength: 10" + "\r" +
            "Ammo: 5" + "\r" +
            "Fuel: NA"
        app.MainPage.DisplayAlert(unitName, unitInformation, "OK") |> ignore

    do
        let stack = new StackLayout()
        let label = new Label()
        label.Text <- "Hello"
        stack.Children.Add(label)
        let button = new Button()
        button.Text <- "Click Me"
        button.Clicked.Add(button_Clicked)
        stack.Children.Add(button)
        base.MainPage <- ContentPage(Content = stack)


