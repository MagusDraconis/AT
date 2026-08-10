namespace TQM.Core.ResearchQM;

public sealed record EquivCheck(string Aspect,string StandardQM,string Tqm,string Identical,string Status);
public sealed record ActResidue(string Effect,string Mechanism,string Magnitude,string Testable,string Status);
public sealed record DecoPred(string Prediction,string Scale,string Constraint,string Testable,string Status);
public sealed record QmExpConstraint(string Experiment,string Precision,string RulesOut,string Status);
public sealed record FalsifyPath(string Test,string WhatItWouldShow,string Feasibility,string Timeline,string Priority);
public sealed record NovelPred(string Prediction,string Category,string Testable,string Distinct,string Timeline,string Status);
public sealed record QMNovelResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,EquivCheck[] EC,ActResidue[] AR,DecoPred[] DP,QmExpConstraint[] XC,FalsifyPath[] FP,NovelPred[] NP);