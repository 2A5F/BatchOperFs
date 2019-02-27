module Tests

open System
open Xunit
open BatchOper.Or

type CountClass<'a>(v: 'a) =
    let mutable count = 0
    member __.Count = count
    member __.a
        with get() = 
            printfn "count: %i" count
            count <- count + 1
            v
let inline CountClassOf v = new CountClass<'a>(v)

[<Fact>]
let BatchOr () =
    let tc = CountClassOf 1
    let out = 1 |> batchOr { eq tc.a; eq tc.a; eq tc.a; }
    Assert.True(out)
    Assert.Equal(tc.Count, 1)
    
