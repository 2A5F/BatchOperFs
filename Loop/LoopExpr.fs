module BatchOper.LoopExpr

open BatchOper.BreakOrContinue

type Labels =
| Label of string

type Foo() =
    let loopexpr = LoopBuilder()
    let aa() = async { return 1 }
    let a() = async {
        if (1 * 8 > 0) then
            let! v = aa()
            return v
        else 
            return 0
    }
    let foo() = loopexpr {
        let x = []
        yield! 1
        for a in x do
        ()
    }
and LoopBuilder() =
    member __.Yield v = v
    member __.Zero () = ()
    member __.For(e, f) = f
    member __.YieldFrom v = ()
    member __.Combine a b = ()
    member __.Delay e = ()