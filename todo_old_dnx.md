# [fix]

* errors not thrown in then
* when no tests found, should print `no specs found`
* just use PASS and FAIL while running tests, all other messages should use the normal info/warn/error log print
* runner should get automatically it's version and print it correctly in the header
* extend all the tests methods with :
  * nice print
  * parameters
* should only print result when all children executed

# [feat]
* enhance loading pipeline:
  * register current loader to loaders convention
  * add ability to add or remove new loaders at startup.
* loaders should found a name for the spec, then print it correctly (print `Hello Specs found` instead of `HelloSpecs found`)
* command line arguments can accept arguments to run a named spec only
* command line arguments can let you choose between normal mode (show only results counters and errors) and verbose mode (show all the specs features)
* command line arguments should allow to print verbose results to a file
* regarding previous feat, that means we need to have a log pipeline with multiple outputs possible
* better header with ascii art ;-)

# [clean]
* run tests on constructor, avoid running them on run method.

# [ideas]
* find a way to introduce cinefin in order to give a difficulty context for the test ((disorder) > chaotic > complex > complicated > obvious)
* we need a context. maybe 2. one is technical, share data between spec conditions. one is more functional documentation, where that spec takes place

* ✔ ✘
