using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.Text;
using System.ComponentModel;
using System.Numerics;

namespace neoSmartAddress
{
    public class NeoSmartAddress : SmartContract
    {
        /// <summary>
        /// NEOSmartAddress
        /// This is the Smart-Contract invocation point
        /// In the Smart-Contract deployment or invocation, you need to specify the parameters of the smart contract.
        /// Smart contract parameters are byte types.
        /// You can find the full definition of the byte types at docs.neo.org - "Parameters and Return Values"
        /// Use 2 hexadecimal characters for each parameter.
        /// 
        /// Params: 0710, return: 05
        /// </summary>
        /// 
        /// <param name="operation">
        ///  The name of the invoking operation.
        /// </param>
        /// 
        /// <param name="args">
        ///  Input arguments for the invoked operation.
        /// </param>s
        public static object Main(string operation, params object[] args)
        {
            switch (operation)
            {
                case "create":
                    return Create((string)args[0], (byte[])args[1]); 	// Create a NeoSmartAddress (string) for the given NeoAddress (byte[])
                case "read":
                    return Read((string)args[0]); 						// Get Neo-Address by NeoSmartAddress(string)
                case "update":
                    return Update((string)args[0], (string)args[1]);	// Change the NeoSmartAddress at 1 (string) to the given new NeoSmarAddress (string)
                case "delete":
                    return Delete((string)args[0]);                     // Delete the NeoSmartAddress (string) for the given NeoAddress (byte[])
                case "isFree":
                    return IsFree((string)args[0]);						// Check if the given NeoSmartAddress (string) is free
                default:
                    return false;
            }
        }


        private static bool Create(string neoSmartAddress, byte[] neoAddress)
        {
            if (!Runtime.CheckWitness(neoAddress)) return false;                    // Check if the owner of neoAddress is the caller of this method 
            byte[] free = Storage.Get(Storage.CurrentContext, neoAddress);	 		// Get the store of the given neoAddress
            if (free != null) return false;                                         // Check if the neoAddress has already created a neoSmartAddress (use update instead of create) 
            free = Storage.Get(Storage.CurrentContext, neoSmartAddress); 			// Get the store of the desired neoSmartAddress
            if (free != null) return false;											// Check if the neoSmartAddress is already taken
            Storage.Put(Storage.CurrentContext, neoSmartAddress, neoAddress);       // Save the desired neoSmartAddress for given neoAddress
            Storage.Put(Storage.CurrentContext, neoAddress, neoSmartAddress);		// Save the neoSmartAddress also to the key of neoAddress 
            return true;
        }


        private static byte[] Read(string neoSmartAddress)
        {
            return Storage.Get(Storage.CurrentContext, neoSmartAddress); 	// Return the neoAddress (byte[]) for the given neoSmartAddress (string)
        }


        private static bool Update(string neoSmartAddressFrom, string neoSmartAddressTo)
        {
            byte[] neoAddress = Storage.Get(Storage.CurrentContext, neoSmartAddressTo);     // Check if new neoSmartAddress is free
            if (neoAddress != null) return false;                                           // The desired neoSmartAddress is already taken
            neoAddress = Storage.Get(Storage.CurrentContext, neoSmartAddressFrom);          // Get the owner of the neoSmartAddressFrom
            if (neoAddress == null) return false;                                           // Actual neoSmartAddress empty - no owner -
            if (!Runtime.CheckWitness(neoAddress)) return false; 						    // Check if the owner of neoAddress is the caller of this method
            Storage.Put(Storage.CurrentContext, neoSmartAddressTo, neoAddress);             // Update store			
            Storage.Put(Storage.CurrentContext, neoAddress, neoSmartAddressTo);             // Update store
            Storage.Delete(Storage.CurrentContext, neoSmartAddressFrom);				    // Delete old Address
            return true;
        }

        private static bool Delete(string neoSmartAddress)
        {
            byte[] ownerOfNeoSmartAddress = Storage.Get(Storage.CurrentContext, neoSmartAddress);	// Get the owner of the neoSmartAddress
            if (ownerOfNeoSmartAddress == null) return false;										// Can not be deleted because neoSmartAddress is free
            if (!Runtime.CheckWitness(ownerOfNeoSmartAddress)) return false;						// Check if the owner of neoSmartAddress is the caller of this method
            Storage.Delete(Storage.CurrentContext, neoSmartAddress);                                // Delete from store 
            Storage.Delete(Storage.CurrentContext, ownerOfNeoSmartAddress);							// Delete from store
            return true;
        }

        private static bool IsFree(string neoSmartAddress)
        {
            byte[] free = Storage.Get(Storage.CurrentContext, neoSmartAddress);   // Check if new neoSmartAddress is free
            if (free != null) return false;									      // The desired neoSmartAddress is already taken
            return true;														  // Else it is free	
        }
    }
}
