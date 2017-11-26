using JS.Core.Foundation.Data;
using JS.Suite.BusinessLogic.JSWedding.Generated;
using JS.Suite.DataAbstraction.JSWedding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.JSWedding
{
    /// <summary>
    /// Wedding Party Member Business Manager
    /// </summary>
    public class WeddingPartyMemberBusinessManager : WeddingPartyMemberBusinessManager_Generated<WeddingPartyMemberBusinessManager>
    {
        /// <summary>
        /// Selects the with summary partners asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<WeddingPartyMember>> SelectWithSummaryPartnersAsync(IConnectionInfo connectionInfo, WeddingPartyMember filter)
        {
            return await WeddingPartyMemberManager.Current.SelectWithSummaryPartnersAsync(connectionInfo, filter);
        }

        /// <summary>
        /// Selects the type of the with summary partners and group by.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<List<WeddingPartyMember>>> SelectWithSummaryPartnersAndGroupByType(IConnectionInfo connectionInfo, WeddingPartyMember filter)
        {
            return (await SelectWithSummaryPartnersAsync(connectionInfo, filter))
                .GroupBy(wpm => new {wpm.WeddingPartyMemberTypeId, wpm.WeddingPartyMemberTypePriority})
                .Select(g => g.First()).ToList()
                .GroupBy(wpm => wpm.WeddingPartyMemberTypeId)
                .Select(g => g.ToList())
                .ToList();
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="weddingPartyMember">The model.</param>
        /// <returns></returns>
        public string GetFullName(WeddingPartyMember weddingPartyMember)
        {
            if (weddingPartyMember.SummaryPartnerId == default(int))
            {
                return String.Format("{0} {1}", weddingPartyMember.NameFirst.ToUpper(), weddingPartyMember.NameLast.ToUpper()); 
            }
            else
            {
                return String.Format("{0} {1} AND {2} {3}",
                    weddingPartyMember.NameFirst.ToUpper(),
                    weddingPartyMember.NameLast != weddingPartyMember.SummaryPartner.NameLast ? weddingPartyMember.NameLast.ToUpper() : String.Empty,
                    weddingPartyMember.SummaryPartner.NameFirst.ToUpper(),
                    weddingPartyMember.SummaryPartner.NameLast).ToUpper();
            }
        }
    }
}
