
open System
open System.IO
open System.Drawing

let drawImage (sourceImage:Image) (bitmap:Bitmap) =
    let graphics = Graphics.FromImage(bitmap)
    let targetRectangle = new Rectangle(0,0,bitmap.Width,bitmap.Height)
    let sourceRectangle = new Rectangle(bitmap.Width * 3,bitmap.Height * 3,bitmap.Width,bitmap.Height)
    graphics.DrawImage(sourceImage,targetRectangle,sourceRectangle,GraphicsUnit.Pixel)
    graphics.Flush()

let path = "//Users//jamesdixon//GitHub//ChickenSoftware.PhotoParser//ChickenSoftware.PhotoParser//images//tacmap_dry.png"
let width = 60
let height = 50
let sourceImage = Image.FromFile(path)
let bitmap = new Bitmap(60,50)
drawImage sourceImage bitmap

let getCoordinates index (bitmap:Bitmap) =
    let y = index / bitmap.Width
    let x = index % bitmap.Width
    x,y

let updateColor (color:Color) =
    match color.R, color.G, color.B with
    | 255uy,225uy,225uy -> Color.FromArgb(0x00FFFFFF)
    | _,_,_ -> color

let adjustColor index (bitmap:Bitmap) =
    let coordinates = getCoordinates index bitmap
    let x = fst coordinates
    let y = snd coordinates
    let color = bitmap.GetPixel(x,y)
    let updatedColor = updateColor color
    bitmap.SetPixel(x,y,updatedColor)

let createTransparentBitmap (bitmap:Bitmap) =
    let totalPixels = bitmap.Height * bitmap.Width
    [0 .. totalPixels - 1]
    |> Seq.iter(fun i -> adjustColor i bitmap)
    bitmap

createTransparentBitmap bitmap
let updatedImage = bitmap :> Image
updatedImage

let totalPixels = bitmap.Height * bitmap.Width
[0 .. totalPixels - 1]
|> Seq.map(fun i -> getCoordinates i bitmap)
|> Seq.map(fun (x,y) -> bitmap.GetPixel(x,y))
|> Seq.countBy(fun c -> c.R, c.G, c.B)
|> Seq.sortByDescending(fun x -> snd x)
|> Seq.iter(fun x -> printfn "%A" x)


