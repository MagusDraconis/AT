namespace TQM.Core.ResearchDATA;

public sealed record NovelPrediction(string Aspect,string Tqm,string Mond,string Lcdm,string Status,string Testability,string Priority);
public sealed record RedshiftPoint(double Z,double HZ,double GDagger,double GDagger_1e10,double Scatter,string Notes);
public sealed record RedshiftEvolution(RedshiftPoint[] Points,string Novel,string Mond,string Lcdm);
public sealed record ScatterForecast(double Z,double Sigma,double Current,double Factor,string Mechanism);
public sealed record GalaxyTypePred(string Type,string Scatter,string Gdagger,string TqmMech,string MondPred,string LcdmPred,string Distinctive);
public sealed record FailCond(string Severity,string Condition,string Measurement,string Testability,string Instrument,double Timeline);
public sealed record ObsPrio(string Priority,string Dataset,string Instrument,double Timeline,string What,double Power);
public sealed record NovelResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,NovelPrediction[] P,RedshiftEvolution E,ScatterForecast[] S,GalaxyTypePred[] G,FailCond[] F,ObsPrio[] O);