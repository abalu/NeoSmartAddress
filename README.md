![smartAddressLogo](https://github.com/abalu/NeoSmartAddress/blob/master/img/smartAddress.PNG)

# NeoSmartAddress
Give your NEO-Blockchain public-address a desired alias with this SmartContract <br/>

The NeoSmartAddress contract is simple but powerful. If you invoke the NeoSmartAddress contract from your wallet, you can create an alias for your public-key / wallet address. Every dApp or exchange that supports the NeoSmartAddress can then read your public address if the alias is provided. Precisely that means: everyone that invokes the NeoSmartAddress contract by passing the `read` operation and the given alias can get the belonging public address back from the Smart Contract. Every Neo-Address can only register one identical SmartAddress. You need to be signed-in to the wallet for which you want to create the NeoSmartAddress.  

This has a lot of advantages over the actual public key: 
* NEO user doesn't need to copy-paste public keys from ..where-ever.. anymore
* This Smart-Contract functions as a key store for the user 
* Sharing of a Neo-Address becomes easier
* Memorizing the public address becomes possible, because the user can choose a name
* This contract could become a commonly accepted standard, if trusted wallets implement it 

Of course, it also carries risks and has some downsides:
* A fraud service could just pretend to invoke the Smart Contract and return a random Neo public address.
  Fraud services are always an existing risk. Decentralizing trust means also decentralizing responsibility. To protect from this danger, one could only trust the service if the Transaction-ID which the service gets back when sending the InvocationTransaction to the network is provided. The Transaction-ID can be looked up at the Blockchain by the user. 
* Invoking the Smart Contract will cost a small fee.   

Evolution steps:
We could probably think of extending this Smart Contract to provide more ability for restrictions so that one could decide over the wallets that are allowed to get back the public Address for the given address.   

I have wrote an step-by-step guide to start writing Smart Contracts on the NEO Blockchain. <br/>
You can find it in the Wiki of this Repository - here https://github.com/abalu/NeoSmartAddress/wiki <br/>
My goal is to gather everything that will possibly drive you mad and hold you back from easy start developing.<br/>


