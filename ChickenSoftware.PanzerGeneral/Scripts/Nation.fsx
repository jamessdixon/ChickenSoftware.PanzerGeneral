
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

let nation1 = Nation.Allied France
let nation2 = Nation.Axis German
