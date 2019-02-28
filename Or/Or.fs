namespace BatchOper

type BatchOrBuilder() =
    member __.Yield _ = fun c x -> c x

    member __.Run f = fun x -> (f <| fun _ -> false) x

    [<CustomOperation("eq")>]
    /// Equal
    member __.Equals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() = x || c x

    [<CustomOperation("ne")>]
    /// Not equal
    member __.NotEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <> x || c x

    [<CustomOperation("gt")>]
    /// Greater than
    member __.GreaterThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() > x || c x

    [<CustomOperation("lt")>]
    /// Less than
    member __.LessThan(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() < x || c x

    [<CustomOperation("ge")>]
    /// Greater then or equal
    member __.GreaterEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() >= x || c x

    [<CustomOperation("le")>]
    /// Less then or equal
    member __.LessEquals(f, [<ProjectionParameter>] a) = fun c -> f <| fun x -> a() <= x || c x

module Or =
    /// x |> batchOr { eq a; ne b; gt c; lt d; ge e; le f; }
    ///
    /// is equivalent to
    ///
    /// x = a || x <> b || x > c || x < d || x >= e || x <= f
    let batchOr = BatchOrBuilder()
