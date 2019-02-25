module BatchOper

type OrBuilder() =
    member __.Yield _ = fun c x -> c x

    member __.Run f = fun x -> (f <| fun _ -> false) x

    [<CustomOperation("eq", AllowIntoPattern = true)>]
    member __.Equals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() = x || c x

    [<CustomOperation("ne", AllowIntoPattern = true)>]
    member __.NotEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <> x || c x

    [<CustomOperation("gt", AllowIntoPattern = true)>]
    member __.GreaterThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() > x || c x

    [<CustomOperation("lt", AllowIntoPattern = true)>]
    member __.LessThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() < x || c x

    [<CustomOperation("ge", AllowIntoPattern = true)>]
    member __.GreaterEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() >= x || c x

    [<CustomOperation("le", AllowIntoPattern = true)>]
    member __.LessEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <= x || c x

type AndBuilder() =
    member __.Yield _ = fun c x -> c x

    member __.Run f = fun x -> (f <| fun _ -> true) x

    [<CustomOperation("eq", AllowIntoPattern = true)>]
    member __.Equals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() = x && c x

    [<CustomOperation("ne", AllowIntoPattern = true)>]
    member __.NotEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <> x && c x

    [<CustomOperation("gt", AllowIntoPattern = true)>]
    member __.GreaterThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() > x && c x

    [<CustomOperation("lt", AllowIntoPattern = true)>]
    member __.LessThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() < x && c x

    [<CustomOperation("ge", AllowIntoPattern = true)>]
    member __.GreaterEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() >= x && c x

    [<CustomOperation("le", AllowIntoPattern = true)>]
    member __.LessEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <= x && c x

let batchOr = OrBuilder()
let batchAnd = AndBuilder()
