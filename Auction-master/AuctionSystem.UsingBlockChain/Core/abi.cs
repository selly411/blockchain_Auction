using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.Core
{
    static class ABI
    {
        public static string Deed = @"
            [
	{
		'constant': true,
		'inputs': [
			{
				'name': 'interfaceId',
				'type': 'bytes4'

            }
		],
		'name': 'supportsInterface',
		'outputs': [
			{
				'name': '',
				'type': 'bool'

            }
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [],
		'name': 'name',
		'outputs': [
			{
				'name': '',
				'type': 'string'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'getApproved',
		'outputs': [
			{
				'name': '',
				'type': 'address'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': 'to',
				'type': 'address'
			},
			{
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'approve',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [],
		'name': 'totalSupply',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': 'from',
				'type': 'address'
			},
			{
				'name': 'to',
				'type': 'address'
			},
			{
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'transferFrom',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'owner',
				'type': 'address'
			},
			{
				'name': 'index',
				'type': 'uint256'
			}
		],
		'name': 'tokenOfOwnerByIndex',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': 'from',
				'type': 'address'
			},
			{
				'name': 'to',
				'type': 'address'
			},
			{
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'safeTransferFrom',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'index',
				'type': 'uint256'
			}
		],
		'name': 'tokenByIndex',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'owner',
				'type': 'address'
			}
		],
		'name': 'tokensOf',
		'outputs': [
			{
				'name': '',
				'type': 'uint256[]'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'ownerOf',
		'outputs': [
			{
				'name': '',
				'type': 'address'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'owner',
				'type': 'address'
			}
		],
		'name': 'balanceOf',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [],
		'name': 'symbol',
		'outputs': [
			{
				'name': '',
				'type': 'string'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': 'to',
				'type': 'address'
			},
			{
				'name': 'approved',
				'type': 'bool'
			}
		],
		'name': 'setApprovalForAll',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': 'from',
				'type': 'address'
			},
			{
				'name': 'to',
				'type': 'address'
			},
			{
				'name': 'tokenId',
				'type': 'uint256'
			},
			{
				'name': '_data',
				'type': 'bytes'
			}
		],
		'name': 'safeTransferFrom',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'tokenURI',
		'outputs': [
			{
				'name': '',
				'type': 'string'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': '_tokenId',
				'type': 'uint256'
			},
			{
				'name': '_uri',
				'type': 'string'
			}
		],
		'name': 'addDeedMetadata',
		'outputs': [
			{
				'name': '',
				'type': 'bool'
			}
		],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': 'owner',
				'type': 'address'
			},
			{
				'name': 'operator',
				'type': 'address'
			}
		],
		'name': 'isApprovedForAll',
		'outputs': [
			{
				'name': '',
				'type': 'bool'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': '_tokenId',
				'type': 'uint256'
			},
			{
				'name': '_uri',
				'type': 'string'
			}
		],
		'name': 'registerDeed',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'inputs': [
			{
				'name': '_name',
				'type': 'string'
			},
			{
				'name': '_symbol',
				'type': 'string'
			}
		],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'constructor'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': false,
				'name': '_by',
				'type': 'address'
			},
			{
				'indexed': false,
				'name': '_tokenId',
				'type': 'uint256'
			}
		],
		'name': 'DeedRegistered',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': true,
				'name': 'from',
				'type': 'address'
			},
			{
				'indexed': true,
				'name': 'to',
				'type': 'address'
			},
			{
				'indexed': true,
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'Transfer',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': true,
				'name': 'owner',
				'type': 'address'
			},
			{
				'indexed': true,
				'name': 'approved',
				'type': 'address'
			},
			{
				'indexed': true,
				'name': 'tokenId',
				'type': 'uint256'
			}
		],
		'name': 'Approval',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': true,
				'name': 'owner',
				'type': 'address'
			},
			{
				'indexed': true,
				'name': 'operator',
				'type': 'address'
			},
			{
				'indexed': false,
				'name': 'approved',
				'type': 'bool'
			}
		],
		'name': 'ApprovalForAll',
		'type': 'event'
	}
]";

        public static string Auction = @"
            [
	{
		'constant': true,
        'inputs': [
			{
				'name': '_auctionId',
                'type': 'uint256'

            }
		],
		'name': 'getAuctionById',
		'outputs': [
			{
				'name': 'name',
				'type': 'string'

            },
			{
				'name': 'blockDeadline',
				'type': 'uint256'
			},
			{
				'name': 'startPrice',
				'type': 'uint256'
			},
			{
				'name': 'metadata',
				'type': 'string'
			},
			{
				'name': 'deedId',
				'type': 'uint256'
			},
			{
				'name': 'deedRepositoryAddress',
				'type': 'address'
			},
			{
				'name': 'owner',
				'type': 'address'
			},
			{
				'name': 'active',
				'type': 'bool'
			},
			{
				'name': 'finalized',
				'type': 'bool'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'bidOnAuction',
		'outputs': [],
		'payable': true,
		'stateMutability': 'payable',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'getBidsCount',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'name': 'auctions',
		'outputs': [
			{
				'name': 'name',
				'type': 'string'
			},
			{
				'name': 'blockDeadline',
				'type': 'uint256'
			},
			{
				'name': 'startPrice',
				'type': 'uint256'
			},
			{
				'name': 'metadata',
				'type': 'string'
			},
			{
				'name': 'deedId',
				'type': 'uint256'
			},
			{
				'name': 'deedRepositoryAddress',
				'type': 'address'
			},
			{
				'name': 'owner',
				'type': 'address'
			},
			{
				'name': 'active',
				'type': 'bool'
			},
			{
				'name': 'finalized',
				'type': 'bool'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': '',
				'type': 'uint256'
			},
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'name': 'auctionBids',
		'outputs': [
			{
				'name': 'from',
				'type': 'address'
			},
			{
				'name': 'amount',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'cancelAuction',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [],
		'name': 'getCount',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': '_owner',
				'type': 'address'
			}
		],
		'name': 'getAuctionsOf',
		'outputs': [
			{
				'name': '',
				'type': 'uint256[]'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': '',
				'type': 'address'
			},
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'name': 'auctionOwner',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': '_owner',
				'type': 'address'
			}
		],
		'name': 'getAuctionsCountOfOwner',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': true,
		'inputs': [
			{
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'getCurrentBid',
		'outputs': [
			{
				'name': '',
				'type': 'uint256'
			},
			{
				'name': '',
				'type': 'address'
			}
		],
		'payable': false,
		'stateMutability': 'view',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': '_deedRepositoryAddress',
				'type': 'address'
			},
			{
				'name': '_deedId',
				'type': 'uint256'
			},
			{
				'name': '_auctionTitle',
				'type': 'string'
			},
			{
				'name': '_metadata',
				'type': 'string'
			},
			{
				'name': '_startPrice',
				'type': 'uint256'
			},
			{
				'name': '_blockDeadline',
				'type': 'uint256'
			}
		],
		'name': 'createAuction',
		'outputs': [
			{
				'name': '',
				'type': 'bool'
			}
		],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'constant': false,
		'inputs': [
			{
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'finalizeAuction',
		'outputs': [],
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'payable': false,
		'stateMutability': 'nonpayable',
		'type': 'fallback'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': false,
				'name': '_from',
				'type': 'address'
			},
			{
				'indexed': false,
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'BidSuccess',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': false,
				'name': '_owner',
				'type': 'address'
			},
			{
				'indexed': false,
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'AuctionCreated',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': false,
				'name': '_owner',
				'type': 'address'
			},
			{
				'indexed': false,
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'AuctionCanceled',
		'type': 'event'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': false,
				'name': '_owner',
				'type': 'address'
			},
			{
				'indexed': false,
				'name': '_auctionId',
				'type': 'uint256'
			}
		],
		'name': 'AuctionFinalized',
		'type': 'event'
	}
]";

    }
}
