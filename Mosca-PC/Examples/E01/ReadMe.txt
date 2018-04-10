例子1

5子通道4燃料棒计算模型，堆芯单组件子通道分析

使用自定义物性材料

稳态计算

W3-DNBR计算公式









<Material Index="12" Name="ZirClad" Type="UserDefine">
      <K Value="0">
        <Data T="0" Value="7.93"/>
        <Data T="100" Value="10.81"/>
        <Data T="200" Value="13.21"/>
        <Data T="300" Value="15.22"/>
        <Data T="400" Value="16.93"/>
        <Data T="500" Value="18.45"/>
        <Data T="600" Value="19.85"/>
        <Data T="700" Value="21.24"/>
        <Data T="800" Value="22.71"/>
        <Data T="900" Value="24.35"/>
        <Data T="1000" Value="26.25"/>
        <Data T="1100" Value="28.51"/>
        <Data T="1200" Value="31.22"/>
        <Data T="1300" Value="34.47"/>
        <Data T="1400" Value="38.36"/>
        <Data T="1500" Value="42.99"/>
      </K>
      <Cp Value="0">
        <Data T="0" Value="286.5"/>
        <Data T="100" Value="296.5"/>
        <Data T="200" Value="306.5"/>
        <Data T="300" Value="316.5"/>
        <Data T="400" Value="326.5"/>
        <Data T="500" Value="336.5"/>
        <Data T="600" Value="346.5"/>
        <Data T="700" Value="356.5"/>
        <Data T="800" Value="360"/>
        <Data T="2000" Value="360"/>
      </Cp>
    </Material>