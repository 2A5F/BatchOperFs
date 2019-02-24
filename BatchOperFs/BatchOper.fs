module BatchOper

type OperBuilder(logic: bool -> bool -> bool) =

    member inline __.Yield _ = fun _ _ -> false

    member __.Run (f) = fun x -> f x logic

    [<CustomOperation("eq", AllowIntoPattern = true)>]
    member __.Equals(f, [<ProjectionParameter>] a) = fun x logic -> logic (f x logic) (a() = x) 

    [<CustomOperation("ne", AllowIntoPattern = true)>]
    member __.NotEquals(f, [<ProjectionParameter>] a) = fun x logic -> logic (f x logic) (a() <> x) 

    [<CustomOperation("gt", AllowIntoPattern = true)>]
    member __.GreaterThan(f, [<ProjectionParameter>] a) = fun x logic -> logic (f x logic) (a() > x)

    [<CustomOperation("lt", AllowIntoPattern = true)>]
    member __.LessThan(f, [<ProjectionParameter>] a) = fun x logic -> logic (f x logic) (a() < x)

    [<CustomOperation("ge", AllowIntoPattern = true)>]
    member __.GreaterEquals(f, [<ProjectionParameter>] a) = fun x logic -> logic (f x logic) (a() >= x)

    [<CustomOperation("le", AllowIntoPattern = true)>]
    member __.LessEquals(f, [<ProjectionParameter>] a) = fun x logic -> logic (f x logic) (a() <= x)


type OrBuilder() =
    inherit OperBuilder(fun l r -> l || r)

type AndBuilder() =
    inherit OperBuilder(fun l r -> l && r)

let inline oper logic = OperBuilder(logic)
let orb = OrBuilder()
let andb = AndBuilder()

