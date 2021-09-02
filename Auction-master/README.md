
##### Smart Contract를 새로 배포하면 AuctionSystem.UsingBlockChain.vshost.exe.config 파일을 수정할 것
```xml
<userSettings>
    <AuctionSystem.UsingBlockChain.Properties.Settings>
      <setting name="DeedAddress" serializeAs="String">
        <value>0xD5CdA80bf9c5f943c8e8D0482A44DF996A4DC610</value>
      </setting>
      <setting name="AuctionAddress" serializeAs="String">
        <value>0x0b2daB520303d802F511f4E300bB8AB9B1e41b54</value>
      </setting>
      
    </AuctionSystem.UsingBlockChain.Properties.Settings>
</userSettings>
```

##### 경매 시간이 마감된 물품은 따로 경매종료 버튼을 눌러야 비로소 경매가 종료됨. 경매가 종료된다는것은 이더리움이 판매자에게 전달되고, 증서가 구매자에게 전달된다는 뜻
