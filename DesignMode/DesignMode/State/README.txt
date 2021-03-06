﻿状态者模式的介绍
　　每个对象都有其对应的状态，而每个状态又对应一些相应的行为，如果某个对象有多个状态时，那么就会对应很多的行为。
那么对这些状态的判断和根据状态完成的行为，就会导致多重条件语句，并且如果添加一种新的状态时，需要更改之前现有的代码。
这样的设计显然违背了开闭原则。状态模式正是用来解决这样的问题的。状态模式将每种状态对应的行为抽象出来成为单独新的对象，
这样状态的变化不再依赖于对象内部的行为。

状态者模式的定义
　　上面对状态模式做了一个简单的介绍，这里给出状态模式的定义。

　　状态模式——允许一个对象在其内部状态改变时自动改变其行为，对象看起来就像是改变了它的类。

状态者模式的结构
　　既然状态者模式是对已有对象的状态进行抽象，则自然就有抽象状态者类和具体状态者类，而原来已有对象需要保存抽象状态者类的引用，
通过调用抽象状态者的行为来改变已有对象的行为。

状态者模式涉及以下三个角色：

Account类：维护一个State类的一个实例，该实例标识着当前对象的状态。
State类：抽象状态类，定义了一个具体状态类需要实现的行为约定。
SilveStater、GoldState和RedState类：具体状态类，实现抽象状态类的每个行为。

状态者模式的应用场景
 　　在以下情况下可以考虑使用状态者模式。

当一个对象状态转换的条件表达式过于复杂时可以使用状态者模式。把状态的判断逻辑转移到表示不同状态的一系列类中，可以把复杂的判断逻辑简单化。
当一个对象行为取决于它的状态，并且它需要在运行时刻根据状态改变它的行为时，就可以考虑使用状态者模式。

状态者模式的优缺点
 　　状态者模式的主要优点是：

将状态判断逻辑每个状态类里面，可以简化判断的逻辑。
当有新的状态出现时，可以通过添加新的状态类来进行扩展，扩展性好。
　　状态者模式的主要缺点是：

如果状态过多的话，会导致有非常多的状态类，加大了开销。

总结
　　状态者模式是对对象状态的抽象，从而把对象中对状态复杂的判断逻辑已到各个状态类里面，从而简化逻辑判断。