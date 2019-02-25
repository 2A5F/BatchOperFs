// Learn more about F# at http://fsharp.org

open BatchOper
open System

type TheClass() =
    let mutable count = 0
    member __.a 
        with get() = 
            printfn "count: %i" count
            count <- count + 1
            1

let call () =
    let tc = new TheClass()
    let out = 1 |> batchOr { eq tc.a; eq tc.a; eq tc.a; }
    out


let foo (x: 't) a b c d e f = 
    let out = x |> batchOr { eq a; ne b; gt c; lt d; ge e; le f; }
    out

[<EntryPoint>]
let main argv =
    
    //printfn "out: %A" <| foo 1 1 1 1 2 2 2 

    printfn "out: %b" <| call() 

    printfn "Hello World from F#!"

    Console.ReadLine() |> ignore
    0 // return an integer exit code
