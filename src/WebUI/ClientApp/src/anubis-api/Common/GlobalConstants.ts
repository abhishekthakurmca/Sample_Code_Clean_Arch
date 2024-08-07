export enum FreightTypes {
  FTL = 'FTL',
  LTL = 'LTL',
  WGS = 'WGS/Decommission',
  OnsiteDestructionErasure = 'Onsite Destruction/Erasure'
}
export class GlobalConstants {
  public static PerPageRecords: number = 15;
  public static WGSStr: any = FreightTypes.WGS.toString(); 
}
