namespace TQM.Core.ResearchQG;

public sealed record YukawaSector(string Name,double Y1,double Y2,double Y3,double SumY,double SumSqrtY,double Q,double AngleDeg,string KoideStatus);
public sealed record MixingGeom(string Matrix,string Connects,string TqmInterpretation,string GeometricObservable);
public sealed record YGResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,YukawaSector[] Sectors,MixingGeom[] Mixings);
