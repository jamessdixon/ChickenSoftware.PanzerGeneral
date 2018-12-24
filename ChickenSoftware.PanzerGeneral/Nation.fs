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
    | Allied OtherAllied -> 0
    | _ -> 10