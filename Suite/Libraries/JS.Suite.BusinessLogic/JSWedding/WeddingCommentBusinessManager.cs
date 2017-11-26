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
    /// Wedding Comment Business Manager
    /// </summary>
    public class WeddingCommentBusinessManager : WeddingCommentBusinessManager_Generated<WeddingCommentBusinessManager>
    {
        private List<string> urlPieces = new List<string>() {
            "http",
            ".com",
            ".net",
            ".org",
            "buy",
            ".info",
            "online",
            ".coop"
        };

        /// <summary>
        /// Determines whether the specified wedding comment is spam.
        /// </summary>
        /// <param name="weddingComment">The wedding comment.</param>
        /// <returns></returns>
        public bool IsSpam(WeddingComment weddingComment)
        {
            foreach (string urlItem in urlPieces)
            {
                if (!String.IsNullOrEmpty(weddingComment.Comment) && weddingComment.Comment.ToLower().Contains(urlItem))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
