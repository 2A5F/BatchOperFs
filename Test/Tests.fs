module Tests

open Xunit
open BatchOper.Or
open BatchOper.And
open BatchOper.Loop
open BatchOper.BreakOrContinue

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
[<Trait("BatchOr","1")>]
let BatchOr () =
    let tc = CountClassOf 1
    let out = 1 |> batchOr { eq tc.a; eq tc.a; eq tc.a; }
    Assert.True(out)
    Assert.Equal(tc.Count, 1)

[<Fact>]
[<Trait("BatchOr","2")>]
let BatchOr2 () =
    let tc = CountClassOf <| fun x -> x
    let out = 1 |> batchOr { eq(tc.a 2); eq(tc.a 1); eq(tc.a 2); }
    Assert.True(out)
    Assert.Equal(tc.Count, 2)

[<Fact>]
[<Trait("BatchOr","3")>]
let BatchOr3 () =
    let tc = CountClassOf <| fun x -> x
    let out = 1 |> batchOr { eq(tc.a 2); eq(tc.a 2); eq(tc.a 1); }
    Assert.True(out)
    Assert.Equal(tc.Count, 3)

[<Fact>]
[<Trait("BatchOr","4")>]
let BatchOr4 () =
    let tc = CountClassOf <| fun x -> x
    let out = 1 |> batchOr { eq(tc.a 2); eq(tc.a 2); eq(tc.a 2); }
    Assert.False(out)
    Assert.Equal(tc.Count, 3)

[<Fact>]
[<Trait("BatchAnd","1")>]
let BatchAnd () =
    let tc = CountClassOf 1
    let out = 1 |> batchAnd { eq tc.a; eq tc.a; eq tc.a; }
    Assert.True(out)
    Assert.Equal(tc.Count, 3)

[<Fact>]
[<Trait("BatchAnd","2")>]
let BatchAnd2 () =
    let tc = CountClassOf <| fun x -> x
    let out = 1 |> batchAnd { eq(tc.a 1); eq(tc.a 2); eq(tc.a 2); }
    Assert.False(out)
    Assert.Equal(tc.Count, 2)

[<Fact>]
[<Trait("BatchAnd","3")>]
let BatchAnd3 () =
    let tc = CountClassOf <| fun x -> x
    let out = 1 |> batchAnd { eq(tc.a 1); eq(tc.a 1); eq(tc.a 2); }
    Assert.False(out)
    Assert.Equal(tc.Count, 3)

[<Fact>]
[<Trait("BatchAnd","4")>]
let BatchAnd4 () =
    let tc = CountClassOf <| fun x -> x
    let out = 1 |> batchAnd { eq(tc.a 2); eq(tc.a 2); eq(tc.a 2); }
    Assert.False(out)
    Assert.Equal(tc.Count, 1)

[<Fact(Timeout=500)>]
[<Trait("Loop","Example")>]
let LoopExample () =
    loop <| seq {
        for i in 1..10 -> seq {
            if i < 3 then
                yield Continue
            if i > 5 then
                yield Break
        }
    }

[<Fact(Timeout=500)>]
[<Trait("Loop","1")>]
let Loop () = 
    let before = CountClassOf ()
    let after = CountClassOf ()
    let tc = CountClassOf ()
    loop <| seq {
        for i in 1..10 do
        before.a
        yield seq {
            tc.a
            if i > 5 then
                yield Break
        }
        after.a
    }
    Assert.Equal(tc.Count, 6)
    Assert.Equal(before.Count, 6)
    Assert.Equal(after.Count, 5)

[<Fact(Timeout=500)>]
[<Trait("Loop","2")>]
let LoopContinue () =
    let beforeContinue = CountClassOf ()
    let tc = CountClassOf ()
    loop <| seq {
        for i in 1..10 -> seq {
            beforeContinue.a
            if i < 3 then
                yield Continue
            tc.a
            if i > 5 then
                yield Break
        }
    }
    Assert.Equal(beforeContinue.Count, 6)
    Assert.Equal(tc.Count, 4)
    
