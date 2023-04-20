using TPLDataflowByExample.BufferBlock.BufferBlock;
using TPLDataflowByExample.BufferBlock.DataflowBlockOption;
using TPLDataflowByExample.Cancellation;
using TPLDataflowByExample.Common;
using TPLDataflowByExample.DataflowLinkOption;
using TPLDataflowByExample.ExecutionBlock.ActionBlock;
using TPLDataflowByExample.GroupingBlock.BatchBlock;
using TPLDataflowByExample.GroupingBlock.BatchedJoinBlock;
using TPLDataflowByExample.GroupingBlock.BlockCompletion;
using TPLDataflowByExample.GroupingBlock.GroupingDataflowBlockOption;
using TPLDataflowByExample.GroupingBlock.JoinBlock;
using TPLDataflowByExample.Link;

Console.WriteLine($"Start Main thread id : {Environment.CurrentManagedThreadId}");

/*
 * ActionBlock<T> 有一個輸入且沒有輸出。當需要輸入的資料進行處理，但不需要將其傳遞給其他 Block 時，就會使用它。
 */
//ActionBlockExample1.Run();

/*
 * 在 ActionBlockExample1 範例中，可以看到結果："Done" 被先印出來，是因為 actionBlock 以平行的方式執行於主執行緒。
 */
//ActionBlockExample2.Run();


/*
 * TransformBlock<T1, T2> 與 ActionBlock<T> 相似，不同的是它有一個可以連接到其他 block 的輸出。它相當於一個 Func<T1, T2>。
 * 要從 TransformBlock<T1, T2> 提取資料，可以使用「同步」執行的 Receive() 方法。
 * 如果沒有可用的資料，執行緒將暫停，直到有資料可用。
 */
//TransformBlockExample1.Run();

/*
 * 要從 TransformBlock<T1, T2> 提取資料，可以使用「同步」執行的 Receive() 方法。
 * 在返回的 Task 上調用 Result() 方法會強制程序等待資料可用，使其成為與先前範例具有相同 Console 輸出的同步操作
 */
//TransformBlockExample2.Run();

/*
 * 修改後，可以非同步的從 block 中接收資料。並使用 ContinueWith() 方法使我們的主執行緒繼續執行，而不必等待讀取資料。
 */
//TransformBlockExample3.Run();


/*
 * 使用 ExecutionDataflowBlockOptions 選項物件，傳遞給 blocks 建構函式進行配置。
 * 默認情況下，每個值都會逐個進行處理。MaxDegreeOfParallelism 選項告訴計算機同時並行處理多個值。
 */
//ExecutionDataflowBlockOptionsExample1.Run();

/*
 * 這裡使用 SingleProducerConstrained 來優化程式效率。Benchmark1 是不使用 SingleProducerConstrained，Benchmark2 則有。
 * 
 */
//ExecutionDataflowBlockOptionsExample2.Benchmark1();
//ExecutionDataflowBlockOptionsExample2.Benchmark2();

/*
 * Buffer block 用於儲存或分配資料，他是一個先進先出的資料佇列。
 */
//BufferBlockExample1.Run();


/*
 * BroadcastBlock<T> 可以將一個訊息傳送到多個 block。特別的是它沒有內部的 buffer，如果接收到新訊息，它會取代之前的訊息。
 * 且下游 blocks 不接受訊息，它就不會嘗試重新傳送，只會傳送目前的訊息。
 */
//BroadcastBlockExample1.Run(); 


/*
 * WriteOnceBlock<T> 只會接受它收到的第一個值，並在任何時候要求該值時，返回該值。這個 blocks 對於儲存常數很有用。
 */
//WriteOnceBlockExample1.Run();


/*
 * 每個 block 都有一個內部 buffer，可以控制它。範例中使用 BoundedCapacity 將 buffer 大小設置為 1。這意味著該 block 內部只有一個 buffer slot 可以在開始處理資料之前儲存傳入的資料。
 */
//DataflowBlockOptionsExample1.Run();


/*
 * 使用 MaxMessagesPerTask 設定每個 Task 一奧處理得訊息數量。當執行緒被重複使用時，同一時間同一時間只有一個訊息被處理。
 */
//DataflowBlockOptionsExample2.Run


/*
 * NameFormat 允許那您定義 block 的 debug 時名稱和顯示格式。
 */
//DataflowBlockOptionsExample3.Run();



/*
 * Grouping block 可以將多個資料項目組合成像是列表或元組這樣的容器。
 */
//BatchBlockExample1.Run();


/*
 * JoinBlock<T1, T2> 有兩個輸入（Target1 和 Target2），將它們合併成一個 Tuple<T1, T2>。
 */
//JoinBlockExample1.Run();


/*
 * BatchedJoinBlock<T1, T2> 是將 JoinBlock<T1, T2> 和 BatchBlock<T> 的結合。BatchedJoinBlock 將兩個組合成 Tuple<T1[], T2[]>。
 */
//BatchedJoinBlockExample1.Run();


/*
 * 在 JoinBlock 中有兩個執行模式。貪婪模式（預設）和非貪婪模式。
 * 貪婪模式：
 *      即使無法產生元組，該 Block 也會接受所有提供的輸入。
 * 非貪婪模式：
 *      只有在 Target1 和 Target2 都有等待接受的資料時，它才會接受值。
 */
//GroupingDataflowBlockOptionsExample1.Run();

/*
 * 使用非同步的 SendAsync() 取代同步的 Post()，為了確保 jBlock 是否接受了資料，我們將 SendAsync() 回傳的 Task 加入一個等待執行的 Continuation
 */
//GroupingDataflowBlockOptionsExample2.Run();


/*
 * 使用 Complete() 方法讓 Dataflow Block 停止處理資料。
 */
//BlockCompletionExample1.Run();


/*
 * 使當前執行緒暫停，直到 block 完成，通常的方法是在返回的任務上調用 Wait() 方法
 */
//BlockCompletionExample2.Run();


/* 1 to 1
 * Link 是 blocks 通訊的手段。
 * 當多個 blocks 連接到輸出時，消息按照它們 link 的順序傳送到每個 block
 */
//LinkExample1.Run();



/* 1 to N
 * Link 是 blocks 通訊的手段。
 * 當多個 blocks 連接到輸出時，消息按照它們 link 的順序傳送到每個 block
 */
//LinkExample2.Run();


/* N to 1
 * 當一個 block 的輸入被連接到多個 source block 時會發生什麼。
 * 來自兩個 source block 的值按時間順序合併到 printBlock 的輸入端。
 */
//LinkExample3.Run();


/* Filter
 * 每個訊息都會被傳遞到 predicate 函數，如果它返回 true，則該訊息將被傳送到連接的 Block。
 * 由於 BroadcastBlock<T> 僅會發送其接收到的最新訊息，因此所有過濾的訊息都將被簡單地丟棄，以防止死鎖。
 */
//LinkExample4.Run();

/* Filter
 * 可以總是插入一個 BroadcastBlock<T>，但是額外的處理會增加開銷並減慢程序的運行速度。
 * NullTarget<T> 會丟棄它接收到的所有消息。
 * 在過濾連接後增加了 link，sourceBlock 將優先嘗試將所有消息發送到 printBlock，然後將所有其他消息發送到 NullTarget<T> block 。
 */
//LinkExample5.Run();

/*
 * 1 to N 工作自動分配
 */
LinkExample6.Run();


/* 
 *  link 可以配置為在其生命週期中僅傳輸特定數量的訊息
 */
//DataflowLinkOptionsExample1.Run();

/* 
 *  預設情況下，Block 嘗試將訊息傳輸到增加的第一個 link。
 *  如果由於任何原因而無法傳輸，則按順序嘗試 link ，直到訊息被接受。每次呼叫 LinkTo() 方法都會將一個新 link 附加到 link 集合中。
 */
//DataflowLinkOptionsExample2.Run();


/* 
 *  Cancel 設置 1s 如果超過，就會丟棄
 */
//CancellationTokenSourceExample1.Run();


Console.WriteLine($"End Main thread id : {Environment.CurrentManagedThreadId}");

Console.ReadKey();