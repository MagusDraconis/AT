namespace TQM.Core.ResearchQG;

public sealed record CFailure(string Aspect,string Cinfinite,string Czero,string Cobs,string Status);
public sealed record CSweep(string Scale,string QM,string GR,string Cosmo,string BH,string Status);
public sealed record CStability(string Structure,string CSmall,string CLarge,string Window,string Status);
public sealed record ThruPut(string Aspect,string CLow,string CHigh,string Maximized,string Status);
public sealed record CSelect(string Mechanism,string Selects,string Unique,string Status);
public sealed record CResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,CFailure[] CF,CSweep[] CS,CStability[] CST,ThruPut[] TP,CSelect[] CSL);