//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc.Internal;
//using System;
//using Microsoft.AspNetCore.Mvc.Core;
//using Evolution.Plugin.Core;

//namespace Evolution.Web
//{
//    public class EvolutionActionSelectorDecisionTreeProvider : IActionSelectorDecisionTreeProvider
//    {
//        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
//        private ActionSelectionDecisionTree _decisionTree;

//        /// <summary>
//        /// Creates a new <see cref="ActionSelectorDecisionTreeProvider"/>.
//        /// </summary>
//        /// <param name="actionDescriptorCollectionProvider">
//        /// The <see cref="IActionDescriptorCollectionProvider"/>.
//        /// </param>
//        public EvolutionActionSelectorDecisionTreeProvider(
//            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
//        {
//            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
//        }

//        /// <inheritdoc />
//        public IActionSelectionDecisionTree DecisionTree
//        {
//            get
//            {
//                var descriptors = _actionDescriptorCollectionProvider.ActionDescriptors;
//                if (descriptors == null)
//                {
//                    throw new InvalidOperationException();
//                    //throw new InvalidOperationException(
//                    //    Resources.FormatPropertyOfTypeCannotBeNull(
//                    //        "ActionDescriptors",
//                    //        _actionDescriptorCollectionProvider.GetType()));
//                }

//                if (_decisionTree == null || descriptors.Version != _decisionTree.Version || GlobalConfiguration.RefreshDecisionTree)
//                {
//                    _decisionTree = new ActionSelectionDecisionTree(descriptors);
//                    if(GlobalConfiguration.RefreshDecisionTree)
//                    {
//                        GlobalConfiguration.RefreshDecisionTree = false;
//                    }
//                }

//                return _decisionTree;
//            }
//        }
//    }
//}
