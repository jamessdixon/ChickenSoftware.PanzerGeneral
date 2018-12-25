module ChickenSoftware.PanzerGeneral.Nation

type AxisNation =
| Bulgaria
| German
| Hungary
| Italy
| Romania

type AlliedNation =
| France
| Greece
| UnitedStates
| Norway
| Poland
| SovietUnion
| GreatBritian
| Yougaslovia
| OtherAllied

type Nation =
| Allied of AlliedNation
| Axis of AxisNation
| Neutral

let getNation nationId =
    match nationId with
    | 2 -> Allied OtherAllied
    | 3 -> Axis Bulgaria
    | 7 -> Allied France
    | 8 -> Axis German
    | 9 -> Allied Greece
    | 10 -> Allied UnitedStates
    | 11 -> Axis Hungary
    | 13 -> Axis Italy
    | 15 -> Allied Norway
    | 16 -> Allied Poland
    | 18 -> Axis Romania
    | 20 -> Allied SovietUnion
    | 23 -> Allied GreatBritian
    | 24 -> Allied Yougaslovia
    | _ -> Neutral

let getFlagId nation =
    match nation with
    // Belgium -> 1
    | Allied Yougaslovia -> 2
    | Allied France -> 6
    | Axis German -> 7
    | Allied Greece -> 8
    | Allied UnitedStates -> 9
    //China -> 11
    | Axis Italy -> 12
    | Allied Norway -> 14
    | Allied Poland -> 15
    | Axis Romania -> 17
    | Allied SovietUnion -> 19
    | Axis Hungary -> 20
    | Allied GreatBritian -> 22
    | Axis Bulgaria -> 23
    | Allied OtherAllied -> 24
    | _ -> 24