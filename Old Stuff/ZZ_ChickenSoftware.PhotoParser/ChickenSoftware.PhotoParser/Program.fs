open System
open System.IO
open System.Drawing

type PictureFile = {FileName:string; NumberOfColumns:int; NumberOfRows:int}

let createTargetDirectory (fileName:string) =
    let targetDirectory = "//Users//jamesdixon//Desktop//Images//" + fileName
    let directoryinfo = new DirectoryInfo(targetDirectory)
    match directoryinfo.Exists with
    | true -> Directory.Delete(targetDirectory)
              targetDirectory
    | false -> Directory.CreateDirectory(targetDirectory) |> ignore
               targetDirectory

let updateColor (color:Color) =
    match color.R, color.G, color.B with
    | 255uy,225uy,225uy -> Color.FromArgb(0x00FFFFFF)
    //| _,_,_ -> Color.FromArgb(50,0,255,0)
    | _,_,_ -> color

let getCoordinates index (bitmap:Bitmap) =
    let y = index / bitmap.Width
    let x = index % bitmap.Width
    x,y

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

let createHexImage sourcePath targetDirectory (fileName:string) x y =
    let width = 60
    let height = 50
    let index = x + (y * 20)
    use sourceImage = Image.FromFile(sourcePath) 
    use bitmap = new Bitmap(width,height)
    use graphics = Graphics.FromImage(bitmap)
    let targetRectangle = new Rectangle(0,0,width,height)
    let sourceRectangle = new Rectangle(width*x,height*y,width,height)
    graphics.DrawImage(sourceImage,targetRectangle,sourceRectangle,GraphicsUnit.Pixel)
    graphics.Flush()
    let transparentBitmap = createTransparentBitmap bitmap
    let fileName = fileName.Replace("_",String.Empty)
    let fileName' = targetDirectory + "//" + fileName + index.ToString() + ".jpeg"
    transparentBitmap.Save(fileName')   

let createHexImages (pictureFile: PictureFile) =
    Console.WriteLine("starting " + pictureFile.FileName)
    let sourceFileName = pictureFile.FileName + ".png"
    let sourcePath = "images//" + sourceFileName
    let targetDirectory = createTargetDirectory pictureFile.FileName
    [0..pictureFile.NumberOfColumns]
    |> Seq.iter(fun x -> [0..pictureFile.NumberOfRows] 
                            |> Seq.iter(fun y -> (createHexImage sourcePath targetDirectory pictureFile.FileName x y)))
                            
let createImages =
    let files = 
        [
        //{FileName="explode";NumberOfColumns=5;NumberOfRows=0}
        //{FileName="flags";NumberOfColumns=24;NumberOfRows=0}
        //{FileName="hexsides";NumberOfColumns=5;NumberOfRows=0}
        //{FileName="stackicn";NumberOfColumns=19;NumberOfRows=2}
        //{FileName="strength";NumberOfColumns=19;NumberOfRows=7}
        //{FileName="tacicons";NumberOfColumns=19;NumberOfRows=12}
        {FileName="tacmap_dry";NumberOfColumns=2;NumberOfRows=2}
        //{FileName="tacmap_dry";NumberOfColumns=19;NumberOfRows=11}
        //{FileName="tacmap_frozen";NumberOfColumns=19;NumberOfRows=11}
        //{FileName="tacmap_muddy";NumberOfColumns=19;NumberOfRows=11}
        ]
    files
    |> Seq.iter(fun f -> createHexImages f)


[<EntryPoint>]
let main argv =
    createImages
    Console.WriteLine("Done")
    0