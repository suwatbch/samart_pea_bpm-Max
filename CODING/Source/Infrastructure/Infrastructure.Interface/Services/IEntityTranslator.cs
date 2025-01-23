//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The IEntityTranslator interface defines the contract to implement an entity translator
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-470-Translating%20Entities.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;

namespace PEA.BPM.Infrastructure.Interface.Services
{
    public interface IEntityTranslator
    {
        bool CanTranslate(Type targetType, Type sourceType);
        bool CanTranslate<TTarget, TSource>();
        object Translate(IEntityTranslatorService service, Type targetType, object source);
        TTarget Translate<TTarget>(IEntityTranslatorService service, object source);
    }
}
