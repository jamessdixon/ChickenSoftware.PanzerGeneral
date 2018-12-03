
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Scripts/Equipment.fsx"

open Equipment

type MovementStats = {
    MovementPoints: int;
    Fuel: int;
}

type CombatStats = {
    Ammo: int;
    Initiative: int;
}

type Unit = {
    Id: int; 
    Name: string; 
    EquipmentType: EquipmentType; 
    MovementStats: MovementStats option;
    CombatStats: CombatStats option}

let equipmentType = EquipmentType.Transport Transport.Land
let transportUnit = {
    Id = 0; 
    Name = "Test"
    EquipmentType = equipmentType;
    MovementStats = None;
    CombatStats = None}

transportUnit.GetType()
equipmentType.GetType()



