// Learn more about F# at http://fsharp.org

open BatchOper

let foo x a b c d e f = 
    let out = x |> orb { eq a; ne b; gt c; lt d; ge e; le f; }
    out

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
